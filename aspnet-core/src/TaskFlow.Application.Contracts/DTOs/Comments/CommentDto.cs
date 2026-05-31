using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TaskFlow.DTOs.Comments
{
    public class CommentDto : CreationAuditedEntityDto<Guid>
    {
        public Guid TaskId { get; set; }
        public string Content { get; set; }
        public string CreatorUserName { get; set; }
    }
}
