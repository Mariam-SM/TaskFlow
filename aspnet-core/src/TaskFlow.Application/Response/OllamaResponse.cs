using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaskFlow.Response
{
    public class OllamaResponse
    {
        [JsonPropertyName("response")]
        public string Response { get; set; }

        [JsonPropertyName("done")]
        public bool Done { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }
    }
}
