using System;
using System.Collections.Generic;
using TaskFlow.Entities.TaskItems;
using TaskFlow.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace TaskFlow.Entities.Projects
{
    public class Project : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }

        Project()
        {
            // For ORM
        }
        public Project(Guid Id, string name, string description, ProjectStatus status, DateTime? startDate, DateTime? endDate)
            : base(Id)
        {
            Name = name; 
            Description = description; 
            Status = status; 
            StartDate = startDate; 
            EndDate = endDate;
        }
    }
}
