using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.DTOs.Tasks;

namespace TaskFlow.IServices
{
    public interface IAiService
    {
        Task<string> SummarizeTasksAsync(List<TaskDto> tasks);
        Task<string> AskAsync(string prompt);

        //Task<string> AskWithContextAsync(string question, List<TaskDto> tasks);

        //Task<string> AskWithRagContextAsync(string question, string context);
    }
}
