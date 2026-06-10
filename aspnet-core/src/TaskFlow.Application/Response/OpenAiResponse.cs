using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Response
{
    public class OpenAiResponse
    {
        public List<Choice> Choices { get; set; } = [];
    }

    public class Choice
    {
        public Message Message { get; set; } = new();
    }

    public class Message
    {
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
