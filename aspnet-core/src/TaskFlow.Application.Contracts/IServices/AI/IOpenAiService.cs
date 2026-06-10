using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.IServices.AI
{
    public interface IOpenAiService
    {
        Task<string> AskAsync(string prompt);
    }
}
