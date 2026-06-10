using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Entities.TaskItems;
using TaskFlow.Enums;
using TaskFlow.IServices.AI;
using TaskFlow.IServices.AI.Embeddings;
using TaskFlow.IServices.AI.RAG;
using TaskFlow.IServices.AI.Vector;
using Volo.Abp.Domain.Repositories;

namespace TaskFlow.Ai.RAG
{
    public class TaskRagService : ITaskRagService
    {
        private readonly IRepository<TaskItem, Guid> _taskRepository;
        private readonly IEmbeddingService _embeddingService;
        private readonly ITaskVectorStore _vectorStore;
        private readonly IIntentDetectorService _intentDetector;

        public TaskRagService(
            IRepository<TaskItem, Guid> taskRepository,
            IEmbeddingService embeddingService,
            ITaskVectorStore vectorStore,
            IIntentDetectorService intentDetector)
        {
            _taskRepository = taskRepository;
            _embeddingService = embeddingService;
            _vectorStore = vectorStore;
            _intentDetector = intentDetector;
        }

        public async Task<List<TaskItem>> GetRelevantTasksAsync(string query)
        {
            var intent =
                _intentDetector.Detect(query);

            switch (intent)
            {
                case IntentType.Structured:

                    return await GetStructuredTasks(query);

                case IntentType.Semantic:

                    return await GetSemanticTasks(query);

                case IntentType.Hybrid:

                    var structured =
                        await GetStructuredTasks(query);

                    var semantic =
                        await GetSemanticTasks(query);

                    return structured
                        .Concat(semantic)
                        .DistinctBy(x => x.Id)
                        .ToList();

                default:

                    return new List<TaskItem>();
            }
        }

        private async Task<List<TaskItem>> GetStructuredTasks(string query)
        {
            query = query.ToLower();

            if (query.Contains("high priority"))
            {
                return await _taskRepository.GetListAsync(
                    x => x.TaskPriority ==
                         TaskPriority.High);
            }

            if (query.Contains("completed"))
            {
                return await _taskRepository.GetListAsync(
                    x => x.TaskStatus ==
                         TaskItemStatus.Done);
            }

            if (query.Contains("overdue"))
            {
                return await _taskRepository.GetListAsync(
                    x =>
                        x.DueDate < DateTime.Now &&
                        !x.IsCompleted);
            }

            return await _taskRepository.GetListAsync();
        }

        private async Task<List<TaskItem>> GetSemanticTasks(string query)
        {
            var embedding =
                await _embeddingService
                    .GetEmbeddingAsync(query);

            var ids =
                await _vectorStore
                    .SearchAsync(embedding, 5);

            return await _taskRepository
                .GetListAsync(x =>
                    ids.Contains(x.Id));
        }



    }
}