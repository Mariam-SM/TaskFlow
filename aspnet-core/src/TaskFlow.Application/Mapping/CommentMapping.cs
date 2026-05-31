using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.DTOs.Comments;
using TaskFlow.Entities.Comments;

namespace TaskFlow.Mapping
{
    public class CommentMapping : Profile
    {
        public CommentMapping() 
        {
            CreateMap<CreateCommentDto, Comment>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Task, opt => opt.Ignore());

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.CreatorUserName, opt => opt.Ignore());                ;
        }
    }
}
