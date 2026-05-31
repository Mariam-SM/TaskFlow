using System;
using TaskFlow.Entities.TaskItems;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace TaskFlow.Entities.Comments
{
    public class Comment : CreationAuditedEntity<Guid>
    {
        public Guid TaskId { get; set; }
        public TaskItem Task { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; } 
    }
}
