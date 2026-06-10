using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.TaskItems;

namespace TaskFlow.IServices.AI.Indexing
{
    public interface ITaskEmbeddingIndexer
    {
        Task IndexTaskAsync(TaskItem task);
    }
}
