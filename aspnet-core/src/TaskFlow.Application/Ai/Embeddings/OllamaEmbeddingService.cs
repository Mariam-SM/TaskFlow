using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskFlow.IServices.AI.Embeddings;
using TaskFlow.IServices.AI.Vector;
using TaskFlow.Response;

namespace TaskFlow.Ai.Embeddings
{
    public class OllamaEmbeddingService : IEmbeddingService
    {
        private readonly HttpClient _httpClient;
        public OllamaEmbeddingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<float[]> GetEmbeddingAsync(string text)
        {
            var request = new
            {
                model = "nomic-embed-text",
                input = text
            };

            var json = JsonSerializer.Serialize(request);

            var response = await _httpClient.PostAsync(
                "http://localhost:11434/api/embed",
                new StringContent(json, Encoding.UTF8, "application/json"));

            
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);

            var parsed = JsonSerializer.Deserialize<EmbeddingResponse>(result);
            Console.WriteLine(parsed?.Embeddings?.Count);
            Console.WriteLine(parsed?.Embeddings?.FirstOrDefault()?.Length);

            return parsed?.Embeddings?.FirstOrDefault()?? [];
        }
    }
}
