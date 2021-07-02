using FL.Pereza.Helpers.Standard.Enums;
using FL.Web.API.Core.Post.Interactions.Application.Exceptions;
using FL.Web.API.Core.Post.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using FL.Web.API.Core.Post.Interactions.Domain.Enum;
using FL.Web.API.Core.Post.Interactions.Domain.Repositories;
using FL.Web.API.Core.Post.Interactions.Infrastructure.ServiceBus.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Application.Services.Implementations
{
    public class VotePostService : IVotePostService
    {
        private readonly IServiceBusVotePostTopicSender<VotePostBaseDto> iServiceBusVotePostTopicSender;
        private readonly IVotePostRepository iVotePostRepository;

        public VotePostService(
            IServiceBusVotePostTopicSender<VotePostBaseDto> iServiceBusVotePostTopicSender,
            IVotePostRepository iVotePostRepository)
        {
            this.iVotePostRepository = iVotePostRepository;
            this.iServiceBusVotePostTopicSender = iServiceBusVotePostTopicSender;
        }

        public async Task<VotePost> AddVotePost(VotePostDto votePost)
        {
            votePost.Id = $"{votePost.PostId}_{votePost.UserId}";
            votePost.CreationDate = DateTime.UtcNow;
            votePost.Type = ItemHelper.VOTE_POST_TYPE;

            var vote = this.Convert(votePost);
            var result = await this.iVotePostRepository.AddVote(vote);
            await this.iServiceBusVotePostTopicSender.SendMessage(votePost, TopicHelper.LABEL_VOTE_CREATED);

            return result;
        }

        public async Task<bool> DeleteVotePost(string voteId, Guid postId, string userId)
        {
            var vote = await this.iVotePostRepository.GetVoteAsync(voteId, postId);

            if (vote == null)
                return false;

            if (userId == vote.UserId && vote != null)
            {
                var result = await this.iVotePostRepository.DeleteVoteAsync(voteId, vote.PostId);
                if (result)
                {
                    var voteDto = this.Convert(vote);
                    await this.iServiceBusVotePostTopicSender.SendMessage(voteDto, TopicHelper.LABEL_VOTE_DELETED);
                    return true;
                }

                return result;
            }
            else
            {
                throw new UnauthorizedRemove();
            }
        }

        public async Task<IEnumerable<VotePost>> GetVoteByPost(Guid postId)
        {
            return await this.iVotePostRepository.GetVoteByPostAsync(postId);
        }

        private VotePost Convert(VotePostDto source)
        {
            var result = default(VotePost);
            if (source != null)
            {
                result = new VotePost()
                {
                    PostId = source.PostId,
                    SpecieId = source.SpecieId,
                    UserId = source.UserId,
                    Id = source.Id,
                    CreationDate = source.CreationDate,
                    Type = source.Type,
                    AuthorPostId = source.AuthorPostId
                };
            }
            return result;
        }

        private VotePostDto Convert(VotePost source)
        {
            var result = default(VotePostDto);
            if (source != null)
            {
                result = new VotePostDto()
                {
                    PostId = source.PostId,
                    SpecieId = source.SpecieId,
                    UserId = source.UserId,
                    Id = source.Id,
                    CreationDate = source.CreationDate,
                    Type = source.Type,
                    AuthorPostId = source.AuthorPostId
                };
            }
            return result;
        }
    }
}
