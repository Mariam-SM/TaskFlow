using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.DTOs.TaskItems;
using TaskFlow.DTOs.Tasks;
using TaskFlow.Entities.Projects;
using TaskFlow.Entities.TaskItems;
using TaskFlow.Enums;
using TaskFlow.IServices.AI;
using TaskFlow.IServices.AI.Indexing;
using TaskFlow.IServices.AI.RAG;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace TaskFlow.IServices
{
    public class TaskItemAppService : TaskFlowAppService, ITaskItemAppService
    {
        private readonly IRepository<TaskItem, Guid> _taskRepository;
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IIdentityUserRepository _userRepository;
        private readonly IAiService _aiService;
        private readonly ITaskRagService _ragService;
        private readonly ITaskEmbeddingIndexer _embeddingIndexer; 
        private readonly IOpenAiService _openAiService;

        public TaskItemAppService(
            IRepository<TaskItem, Guid> taskRepository,
            IRepository<Project, Guid> projectRepository,
            IIdentityUserRepository userRepository,
            IAiService aiService,
            ITaskRagService ragService,
            ITaskEmbeddingIndexer embeddingIndexer,
            IOpenAiService openAiService)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _aiService = aiService;
            _ragService = ragService;
            _embeddingIndexer = embeddingIndexer;
            _openAiService = openAiService;
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto input)
        {
            var projectExists =
                await _projectRepository.AnyAsync(
                    p => p.Id == input.ProjectId);

            if (!projectExists)
                throw new UserFriendlyException("Project not found.");

            if (input.DueDate.HasValue &&
                input.DueDate.Value < Clock.Now)
            {
                throw new UserFriendlyException("Due date must be in the future.");
            }

            var task = new TaskItem(
                GuidGenerator.Create(),
                input.ProjectId,
                input.Title,
                input.Description,
                input.TaskPriority,
                input.TaskStatus,
                input.DueDate);

            task.ConcurrencyStamp =
                Guid.NewGuid().ToString("N");

            if (input.AssignedToUserId.HasValue)
            {
                task.AssignedToUserId = input.AssignedToUserId.Value;
            }

            // 1. Save in DB first
            task.EmbeddingSerialized = string.Empty;
            await _taskRepository.InsertAsync(task);

            // 2. THEN index in Qdrant
            await _embeddingIndexer.IndexTaskAsync(task);

            return ObjectMapper.Map<TaskItem, TaskDto>(task);
        }

        public async Task<TaskDto> GetAsync(Guid id)
        {
            var task = await _taskRepository.GetAsync(id);

            var project =
                await _projectRepository.GetAsync(task.ProjectId);

            var dto =
                ObjectMapper.Map<TaskItem, TaskDto>(task);

            dto.ProjectName = project.Name;

            if (task.AssignedToUserId.HasValue)
            {
                var user =
                    await _userRepository.FindAsync(task.AssignedToUserId.Value);

                dto.AssignedToUserName = user?.UserName;
            }

            return dto;
        }

        public async Task<PagedResultDto<TaskDto>> GetListAsync(TaskListFilterDto input)
        {
            var query = await _taskRepository.GetQueryableAsync();

            query = query
                .WhereIf(input.ProjectId.HasValue, t => t.ProjectId == input.ProjectId)
                .WhereIf(input.Status.HasValue, t => t.TaskStatus == input.Status)
                .WhereIf(input.Priority.HasValue, t => t.TaskPriority == input.Priority);

            var totalCount = await AsyncExecuter.CountAsync(query);

            var items = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<TaskDto>(
                totalCount,
                ObjectMapper.Map<List<TaskItem>, List<TaskDto>>(items));
        }

        public async Task<TaskDto> UpdateAsync(Guid id, UpdateTaskDto input)
        {
            try
            {
                var task = await _taskRepository.GetAsync(id);

                task.ConcurrencyStamp = input.ConcurrencyStamp;
                task.Title = input.Title;
                task.Description = input.Description;
                task.TaskPriority = input.TaskPriority;
                task.TaskStatus = input.TaskStatus;
                task.DueDate = input.DueDate;
                task.AssignedToUserId = input.AssignedToUserId;
                task.IsCompleted = input.IsCompleted;

                await _taskRepository.UpdateAsync(task, autoSave: true);

                await _embeddingIndexer.IndexTaskAsync(task);

                var dto = ObjectMapper.Map<TaskItem, TaskDto>(task);

                if (task.AssignedToUserId.HasValue)
                {
                    var user =
                        await _userRepository.FindAsync(task.AssignedToUserId.Value);

                    dto.AssignedToUserName = user?.UserName;
                }

                return dto;
            }
            catch (AbpDbConcurrencyException)
            {
                throw new UserFriendlyException("This task was modified by another user.");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async Task<TaskDto> CompleteAsync(Guid id)
        {
            var task = await _taskRepository.GetAsync(id);

            if (string.IsNullOrWhiteSpace(task.Title))
            {
                throw new UserFriendlyException("Cannot complete task without title.");
            }

            task.IsCompleted = true;
            task.TaskStatus = TaskItemStatus.Done;

            await _taskRepository.UpdateAsync(task);

            await _embeddingIndexer.IndexTaskAsync(task);

            return ObjectMapper.Map<TaskItem, TaskDto>(task);
        }

        public async Task<TaskSummaryDto> SummarizeOverdueTasksAsync()
        {
            var tasks =
                await _taskRepository.GetListAsync(x =>
                    x.DueDate < Clock.Now &&
                    !x.IsCompleted &&
                    x.TaskStatus != TaskItemStatus.Done);

            if (!tasks.Any())
            {
                return new TaskSummaryDto
                {
                    Summary = "No overdue tasks."
                };
            }

            var taskDtos =
                ObjectMapper.Map<List<TaskItem>, List<TaskDto>>(tasks);

            var summary =
                await _aiService.SummarizeTasksAsync(taskDtos);

            return new TaskSummaryDto
            {
                Summary = summary
            };
        }

        public async Task<TaskSummaryDto> AskAiAsync(string question)
        {
            var tasks = await _ragService.GetRelevantTasksAsync(question);

            if (!tasks.Any())
            {
                return new TaskSummaryDto
                {
                    Summary = "No relevant tasks found."
                };
            }

            var prompt = new StringBuilder();

            prompt.AppendLine($"Question: {question}");
            prompt.AppendLine("Tasks:");

            foreach (var t in tasks)
            {
                prompt.AppendLine($"""
                Title: {t.Title}
                Description: {t.Description}
                Priority: {t.TaskPriority}
                Status: {t.TaskStatus}
                Due: {t.DueDate}
                """);
            }

            prompt.AppendLine("""
                Answer clearly:
                - insights
                - risks
                - recommendations
                """);

            // Using custom AI service -> Ollama
            var answer = await _aiService.AskAsync(prompt.ToString());

            // Using OpenAI
            //var answer = await _openAiService.AskAsync(prompt.ToString());

            return new TaskSummaryDto
            {
                Summary = answer
            };
        }

    }
}