using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Ai.Embeddings;
using TaskFlow.Entities.TaskItems;
using TaskFlow.IServices.AI.Embeddings;
using TaskFlow.IServices.AI.Indexing;
using TaskFlow.IServices.AI.Vector;

namespace TaskFlow.Ai.Indexing
{
    public class TaskEmbeddingIndexer : ITaskEmbeddingIndexer
    {
        private readonly IEmbeddingService _embeddingService;
        private readonly ITaskVectorStore _vectorStore;

        public TaskEmbeddingIndexer(IEmbeddingService embeddingService, ITaskVectorStore vectorStore)
        {
            _embeddingService = embeddingService;
            _vectorStore = vectorStore;
        }

        public async Task IndexTaskAsync(TaskItem task)
        {
            var text =
                $"""
                Task Title: {task.Title}

                Task Description:
                {task.Description}

                Priority: {task.TaskPriority}

                Status: {task.TaskStatus}

                Due Date: {task.DueDate}
                """;

            var embedding = await _embeddingService.GetEmbeddingAsync(text);
            Console.WriteLine("INDEXING TASK: " + task.Title);
            Console.WriteLine("EMBEDDING LENGTH: " + embedding.Length);

            await _vectorStore.UpsertAsync(task.Id, embedding, task.Title, task.Description);
        }
    }
}
