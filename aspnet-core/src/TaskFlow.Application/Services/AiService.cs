using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskFlow.DTOs.Tasks;
using TaskFlow.IServices;
using TaskFlow.Response;

namespace TaskFlow.Services
{
    public class AiService : IAiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AiService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> AskAsync(string prompt)
        {
            return await SendToOllamaAsync(prompt);
        }

        public async Task<string> SummarizeTasksAsync(List<TaskDto> tasks)
        {
            var prompt = BuildSummaryPrompt(tasks);

            return await SendToOllamaAsync(prompt);
        }

        private async Task<string> SendToOllamaAsync(string prompt)
        {
            var body = new
            {
                model = _configuration["Ollama:Model"],
                prompt = prompt,
                stream = false
            };

            var json =
                JsonSerializer.Serialize(body);

            var response =
                await _httpClient.PostAsync(
                    _configuration["Ollama:BaseUrl"] + "/api/generate",
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"));

            response.EnsureSuccessStatusCode();

            var result =
                await response.Content.ReadAsStringAsync();

            var parsed =
                JsonSerializer.Deserialize<OllamaResponse>(result);

            return parsed?.Response
                   ?? "No AI response generated.";
        }

        private string BuildSummaryPrompt(
            List<TaskDto> tasks)
        {
            var sb = new StringBuilder();

            sb.AppendLine("""
                    Analyze overdue tasks.

                    Return:
                    - Insights
                    - Risks
                    - Recommendations

                    Tasks:
                    """);

            foreach (var task in tasks)
            {
                sb.AppendLine($"""
                    Title: {task.Title}
                    Description: {task.Description}
                    Priority: {task.TaskPriority}
                    Status: {task.TaskStatus}
                    DueDate: {task.DueDate}

                    """);
            }

            return sb.ToString();
        }
    }
}