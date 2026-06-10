using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.DTOs.Tasks;
using TaskFlow.Entities.TaskItems;

namespace TaskFlow.IServices.AI.RAG
{
    public interface ITaskRagService
    {
        Task<List<TaskItem>> GetRelevantTasksAsync(string query);
        //Task<List<TaskItem>> GetRelevantTasksAsync(string query);

        //Task<RagContext> GetContextForAnalysisAsync(string query);
    }
}
