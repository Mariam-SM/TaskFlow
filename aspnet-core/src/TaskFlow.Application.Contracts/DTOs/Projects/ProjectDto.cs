using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Enums;
using Volo.Abp.Application.Dtos;

namespace TaskFlow.DTOs.Projects
{
    public class ProjectDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
