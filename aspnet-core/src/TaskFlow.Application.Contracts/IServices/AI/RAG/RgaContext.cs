using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Entities.TaskItems;

namespace TaskFlow.IServices.AI.RAG
{
    public class RagContext
    {
        public string Query { get; set; }

        public string SystemInsights { get; set; }

        public List<TaskItem> Tasks { get; set; }
    }
}
