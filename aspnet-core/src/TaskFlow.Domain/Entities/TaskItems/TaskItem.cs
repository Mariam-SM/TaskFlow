using System;
using System.Collections.Generic;
using TaskFlow.Entities.Comments;
using TaskFlow.Entities.Projects;
using TaskFlow.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace TaskFlow.Entities.TaskItems
{
    public class TaskItem : FullAuditedEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public TaskItemStatus TaskStatus { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public TaskItem(
                Guid id,
                Guid projectId,
                string title,
                string description,
                TaskPriority taskPriority,
                TaskItemStatus taskStatus,
                DateTime? dueDate)
                : base(id)
            {
                ProjectId = projectId;
                Title = title;
                Description = description;
                TaskPriority = taskPriority;
                TaskStatus = taskStatus;
                DueDate = dueDate;
            }

        }
}
