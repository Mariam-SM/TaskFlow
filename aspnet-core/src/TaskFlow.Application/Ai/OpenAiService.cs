using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskFlow.DTOs.Tasks;
using TaskFlow.IServices;
using TaskFlow.IServices.AI;
using TaskFlow.Response;

namespace TaskFlow.Ai
{
    public class OpenAiService : IOpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public OpenAiService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string> AskAsync(string prompt)
        {
            Console.WriteLine(_configuration["OpenAI:ApiKey"]);
            var request = new
            {
                model = _configuration["OpenAI:Model"],
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                }
            };

            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer",_configuration["OpenAI:ApiKey"]);

            var response = await _httpClient.PostAsJsonAsync(_configuration["OpenAI:BaseUrl"], request);

            var errorBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine(errorBody);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<OpenAiResponse>(json);

            return result?.Choices?.FirstOrDefault()?.Message?.Content?? string.Empty;
        }

    }
}
