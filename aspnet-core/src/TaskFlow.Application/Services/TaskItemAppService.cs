using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.DTOs.TaskItems;
using TaskFlow.DTOs.Tasks;
using TaskFlow.Entities.Projects;
using TaskFlow.Entities.TaskItems;
using TaskFlow.Enums;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
namespace TaskFlow.IServices
{
    public class TaskItemAppService : TaskFlowAppService, ITaskItemAppService
    {
        private readonly IRepository<TaskItem, Guid> _taskRepository;
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IIdentityUserRepository _userRepository;

        public TaskItemAppService(
            IRepository<TaskItem, Guid> taskRepository,
            IRepository<Project, Guid> projectRepository,
            IIdentityUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto input)
        {
            var projectExists = await _projectRepository.AnyAsync(p => p.Id == input.ProjectId);
            if (!projectExists)
                throw new UserFriendlyException("Project not found.");

            if (input.DueDate.HasValue && input.DueDate.Value < Clock.Now)
                throw new UserFriendlyException("Due date must be in the future.");

            var task = new TaskItem(
                GuidGenerator.Create(),
                input.ProjectId,
                input.Title,
                input.Description,
                input.TaskPriority,
                input.DueDate
            );

            //task.AssignedToUserId = input.AssignedToUserId;
            if (input.AssignedToUserId.HasValue)
            {
                task.AssignedToUserId = input.AssignedToUserId.Value;
            }
            await _taskRepository.InsertAsync(task);
            return ObjectMapper.Map<TaskItem, TaskDto>(task);
        }

        public async Task<TaskDto> GetAsync(Guid id)
        {
            var task = await _taskRepository.GetAsync(id);

            var project = await _projectRepository.GetAsync(task.ProjectId);

            string assignedUserName = null;

            if (task.AssignedToUserId.HasValue)
            {
                var user = await _userRepository.GetAsync(task.AssignedToUserId.Value);
                assignedUserName = user.UserName;
            }

            var taskDto = ObjectMapper.Map<TaskItem, TaskDto>(task);

            taskDto.ProjectName = project.Name;

            return taskDto;
        }

        public async Task<PagedResultDto<TaskDto>> GetListAsync(TaskListFilterDto input)
        {
            var query = await _taskRepository.GetQueryableAsync();

            query = query
                .WhereIf(input.ProjectId.HasValue, t => t.ProjectId == input.ProjectId)
                .WhereIf(input.Status.HasValue, t => t.TaskStatus == input.Status)
                .WhereIf(input.Priority.HasValue, t => t.TaskPriority == input.Priority);

            var totalCount = await AsyncExecuter.CountAsync(query);

            var items = await AsyncExecuter.ToListAsync(
                query.OrderBy(t => t.DueDate)
                     .Skip(input.SkipCount)
                     .Take(input.MaxResultCount)
            );

            return new PagedResultDto<TaskDto>(
                totalCount,
                ObjectMapper.Map<List<TaskItem>, List<TaskDto>>(items)
            );
        }

       
        public async Task<TaskDto> UpdateAsync(Guid id, UpdateTaskDto input)
        {
            var task = await _taskRepository.GetAsync(id);

            task.Title = input.Title;
            task.Description = input.Description;
            task.TaskPriority = input.TaskPriority;
            task.TaskStatus = input.TaskStatus;
            task.DueDate = input.DueDate;
            task.AssignedToUserId = input.AssignedToUserId;
            task.IsCompleted = input.IsCompleted;

            await _taskRepository.UpdateAsync(task);
            return ObjectMapper.Map<TaskItem, TaskDto>(task);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async Task<TaskDto> CompleteAsync(Guid id)
        {
            var task = await _taskRepository.GetAsync(id);

            if (string.IsNullOrWhiteSpace(task.Title))
                throw new UserFriendlyException("Cannot complete a task without a title.");

            task.IsCompleted = true;
            task.TaskStatus = TaskItemStatus.Done;

            await _taskRepository.UpdateAsync(task);
            return ObjectMapper.Map<TaskItem, TaskDto>(task);
        }
    }
}
