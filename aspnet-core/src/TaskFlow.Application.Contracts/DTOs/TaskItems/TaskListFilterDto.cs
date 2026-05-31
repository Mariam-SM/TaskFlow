using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Enums;
using Volo.Abp.Application.Dtos;

namespace TaskFlow.DTOs.TaskItems
{
    public class TaskListFilterDto : PagedAndSortedResultRequestDto
    {
        public Guid? ProjectId { get; set; }
        public TaskItemStatus? Status { get; set; }
        public TaskPriority? Priority { get; set; }
    }
}
