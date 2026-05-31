using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Enums;
using Volo.Abp.Application.Dtos;

namespace TaskFlow.DTOs.Tasks
{
    public class TaskDto : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }     
        public Guid AssignedToUserId { get; set; }
        public string AssignedToUserName { get; set; } 
        public TaskPriority TaskPriority { get; set; }
        public TaskItemStatus TaskStatus { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
