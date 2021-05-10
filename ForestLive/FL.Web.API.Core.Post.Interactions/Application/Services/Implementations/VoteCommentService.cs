using FL.Pereza.Helpers.Standard.Enums;
using FL.Web.API.Core.Post.Interactions.Application.Exceptions;
using FL.Web.API.Core.Post.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using FL.Web.API.Core.Post.Interactions.Domain.Enum;
using FL.Web.API.Core.Post.Interactions.Domain.Repositories;
using FL.Web.API.Core.Post.Interactions.Infrastructure.ServiceBus.Contracts;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Application.Services.Implementations
{
    public class VoteCommentService : IVoteCommentService
    {
        private readonly IServiceBusVoteCommentTopicSender<VoteCommentPostDto> iServiceBusVoteCommentTopicSender;
        private readonly IVoteCommentRepository iVoteCommentRepository;
        private readonly ICommentRepository iCommentRepository;

        public VoteCommentService(ICommentRepository iCommentRepository)
        {
            this.iCommentRepository = iCommentRepository;
        }

        public VoteCommentService(
            IServiceBusVoteCommentTopicSender<VoteCommentPostDto> iServiceBusVoteCommentTopicSender,
            ICommentRepository iVotePostRepository,
            IVoteCommentRepository iVoteCommentRepository)
        {
            this.iVoteCommentRepository = iVoteCommentRepository;
            this.iCommentRepository = iVotePostRepository;
            this.iServiceBusVoteCommentTopicSender = iServiceBusVoteCommentTopicSender;
        }

        public async Task<VoteCommentPost> AddVoteCommentPost(VoteCommentPostDto votePost)
        {
            votePost.Id = $"{votePost.CommentId}_{votePost.UserId}";
            votePost.CreationDate = DateTime.UtcNow;
            votePost.Type = ItemHelper.VOTE_COMMENT_TYPE;

            var vote = this.Convert(votePost);
            var result = await this.iVoteCommentRepository.AddCommentVoteAsync(vote);
            if (await this.iCommentRepository.IncreaseVoteCommentCountAsync(votePost.CommentId, vote.PostId)) {
                await this.iServiceBusVoteCommentTopicSender.SendMessage(votePost, TopicHelper.LABEL_VOTE_COMMENT_CREATED);
            }

            return result;
        }


        public async Task<bool> DeleteVoteComment(string voteId, Guid postId, string userId)
        {
            var vote = await this.iVoteCommentRepository.GetVoteAsync(voteId, postId);

            if (vote == null)
                return false;

            if (userId == vote.UserId && vote != null)
            {
                var result = await this.iVoteCommentRepository.DeleteCommentVoteAsync(voteId, vote.PostId);
                result = await this.iCommentRepository.DecreaseVoteCommentCountAsync(vote.CommentId, vote.PostId);
                if (result)
                {
                    var voteDto = this.Convert(vote);
                    await this.iServiceBusVoteCommentTopicSender.SendMessage(voteDto, TopicHelper.LABEL_VOTE_COMMENT_DELETED);
                    return true;
                }

                return result;
            }
            else
            {
                throw new UnauthorizedRemove();
            }
        }


        private VoteCommentPost Convert(VoteCommentPostDto source)
        {
            var result = default(VoteCommentPost);
            if (source != null)
            {
                result = new VoteCommentPost()
                {
                    PostId = source.PostId,
                    UserId = source.UserId,
                    Id = source.Id,
                    CreationDate = source.CreationDate,
                    Type = source.Type,
                    CommentId = source.CommentId
                };
            }
            return result;
        }

        private VoteCommentPostDto Convert(VoteCommentPost source)
        {
            var result = default(VoteCommentPostDto);
            if (source != null)
            {
                result = new VoteCommentPostDto()
                {
                    PostId = source.PostId,
                    UserId = source.UserId,
                    Id = source.Id,
                    CreationDate = source.CreationDate,
                    Type = source.Type,
                    CommentId = source.CommentId
                };
            }
            return result;
        }
    }
}
