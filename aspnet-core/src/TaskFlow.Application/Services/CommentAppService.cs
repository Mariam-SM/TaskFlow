using System;
using System.Threading.Tasks;
using TaskFlow.DTOs.Comments;
using TaskFlow.Entities.Comments;
using TaskFlow.IServices;
using TaskFlow.Services.Comments;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace TaskFlow.Services
{
    public class CommentAppService : CrudAppService<Comment,
        CommentDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateCommentDto>
        , ICommentAppService
    {
        private readonly IRepository<Comment, Guid> _commentRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IIdentityUserRepository _userRepository;

        public CommentAppService(
            IRepository<Comment, Guid> repository,
            ICurrentUser currentUser,
            IIdentityUserRepository userRepository)
            : base(repository)
        {
            _commentRepository = repository;
            _currentUser = currentUser;
            _userRepository = userRepository;
        }

        public override async Task<CommentDto> CreateAsync(CreateCommentDto input)
        {
            if (input.TaskId == Guid.Empty)
                throw new Volo.Abp.UserFriendlyException("TaskId is required!");

            if (!CurrentUser.IsAuthenticated || CurrentUser.Id == null)
                throw new Volo.Abp.Authorization.AbpAuthorizationException("You must be logged in!");

            var comment = ObjectMapper.Map<CreateCommentDto, Comment>(input);
            comment.UserId = CurrentUser.Id.Value;

            await _commentRepository.InsertAsync(comment, autoSave: true);

            var dto = ObjectMapper.Map<Comment, CommentDto>(comment);
            
            var user = await _userRepository.FindAsync(comment.UserId);
            dto.CreatorUserName = user?.UserName ?? "Unknown";

            return dto;
        }

        public override async Task<PagedResultDto<CommentDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var result = await base.GetListAsync(input);

            foreach (var dto in result.Items)
            {
                if (dto.CreatorId.HasValue)
                {
                    var user = await _userRepository.FindAsync(dto.CreatorId.Value);
                    dto.CreatorUserName = user?.UserName ?? "Unknown";
                }
            }

            return result;
        }
    }
}