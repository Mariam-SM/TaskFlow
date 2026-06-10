using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.TaskItems;

namespace TaskFlow.IServices.AI.Vector
{
    public interface ITaskVectorStore
    {
        Task EnsureCollectionExistsAsync();
        Task UpsertAsync(Guid taskId, float[] embedding, string title, string description);
        Task<List<Guid>> SearchAsync(float[] queryEmbedding, int topK = 5);
    }
}
