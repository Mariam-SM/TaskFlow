using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskFlow.DTOs.Comments
{
    public class CreateCommentDto
    {
        [Required]
        public Guid TaskId { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Content { get; set; }
    }
}
