using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskFlow.Enums;
using TaskFlow.IServices.AI;

namespace TaskFlow.Ai
{
    public class IntentDetectorService : IIntentDetectorService
    {
        public IntentType Detect(string query)
        {
            query = query.ToLower();

            var structuredKeywords = new[]
            {
            "high priority",
            "low priority",
            "medium priority",
            "overdue",
            "completed",
            "done",
            "todo",
            "in progress",
            "urgent",
            "status",
            "who has",
            "which tasks",
            "tasks with"
        };

            var semanticKeywords = new[]
            {
            "documentation",
            "similar",
            "related",
            "explain",
            "why",
            "search",
            "find information"
        };

            var hasStructured =
                structuredKeywords.Any(
                    x => query.Contains(x));

            var hasSemantic =
                semanticKeywords.Any(
                    x => query.Contains(x));

            if (hasStructured && hasSemantic)
                return IntentType.Hybrid;

            if (hasStructured)
                return IntentType.Structured;

            if (hasSemantic)
                return IntentType.Semantic;

            return IntentType.Hybrid;
        }
    }
}
