using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaskFlow.Response
{
    public class EmbeddingResponse
    {
        [JsonPropertyName("embeddings")]
        public List<float[]> Embeddings { get; set; }
    }
}
