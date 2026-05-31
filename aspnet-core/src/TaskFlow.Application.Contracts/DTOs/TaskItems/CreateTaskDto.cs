using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskFlow.Enums;

namespace TaskFlow.DTOs.Tasks
{
    public class CreateTaskDto
    {
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
        public Guid? AssignedToUserId { get; set; }

        public TaskPriority TaskPriority { get; set; }
        public TaskItemStatus TaskStatus { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
