using System;
using TaskFlow.DTOs.Comments;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TaskFlow.Services.Comments
{
    public interface ICommentAppService : ICrudAppService<
        CommentDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateCommentDto>
    {
    } 
}