using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.IServices.AI.Embeddings
{
    public interface IEmbeddingService
    {
        Task<float[]> GetEmbeddingAsync(string text);
    }
}
