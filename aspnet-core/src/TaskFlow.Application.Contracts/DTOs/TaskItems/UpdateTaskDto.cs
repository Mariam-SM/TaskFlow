using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskFlow.Enums;
using Volo.Abp.Domain.Entities;

namespace TaskFlow.DTOs.Tasks
{
    public class UpdateTaskDto : IHasConcurrencyStamp
    {
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public Guid AssignedToUserId { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public TaskItemStatus TaskStatus { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
