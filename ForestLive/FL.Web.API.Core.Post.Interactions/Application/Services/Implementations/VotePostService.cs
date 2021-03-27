using FL.Pereza.Helpers.Standard.Enums;
using FL.Web.API.Core.Post.Interactions.Application.Exceptions;
using FL.Web.API.Core.Post.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using FL.Web.API.Core.Post.Interactions.Domain.Enum;
using FL.Web.API.Core.Post.Interactions.Domain.Repositories;
using FL.Web.API.Core.Post.Interactions.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Application.Services.Implementations
{
    public class VotePostService : IVotePostService
    {
        private readonly IServiceBusVotePostTopicSender<VotePostDto> serviceBusVotePostTopicSender;
        private readonly IVotePostRepository votePostRepository;

        public VotePostService(
            IServiceBusVotePostTopicSender<VotePostDto> serviceBusVotePostTopicSender,
            IVotePostRepository votePostRepository)
        {
            this.votePostRepository = votePostRepository;
            this.serviceBusVotePostTopicSender = serviceBusVotePostTopicSender;
        }

        public async Task<VotePost> AddVotePost(VotePost votePost)
        {
            votePost.Id = Guid.NewGuid();
            votePost.CreationDate = DateTime.UtcNow;
            votePost.Type = ItemHelper.VOTE_TYPE;

            var result = await this.votePostRepository.AddVote(votePost);
            var voteDto = this.Convert(result);
            await this.serviceBusVotePostTopicSender.SendMessage(voteDto, TopicHelper.LABEL_VOTE_CREATED);

            return result;
        }

        public async Task<bool> DeleteVotePost(Guid voteId, Guid postId, string userId)
        {
            var vote = await this.votePostRepository.GetVoteAsync(voteId, postId);
            if (userId == vote.UserId && vote != null)
            {
                var result = await this.votePostRepository.DeleteVoteAsync(voteId, vote.PostId);
                if (result)
                {
                    var voteDto = this.Convert(vote);
                    await this.serviceBusVotePostTopicSender.SendMessage(voteDto, TopicHelper.LABEL_VOTE_DELETED);
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
            return await this.votePostRepository.GetVoteByPost(postId);
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
                    AuthorPostUserId = source.AuthorPostUserId,
                    TitlePost = source.TitlePost
                };
            }
            return result;
        }
    }
}
