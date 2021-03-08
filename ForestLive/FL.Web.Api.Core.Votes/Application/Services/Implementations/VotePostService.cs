using FL.Pereza.Helpers.Standard.Enums;
using FL.Web.Api.Core.Votes.Application.Exceptions;
using FL.Web.Api.Core.Votes.Application.Services.Contracts;
using FL.Web.Api.Core.Votes.Domain.Entities;
using FL.Web.Api.Core.Votes.Domain.Enum;
using FL.Web.Api.Core.Votes.Domain.Repositories;
using FL.Web.Api.Core.Votes.Infrastructure.ServiceBus.Contracts;
using FL.Web.API.Core.Votes.Domain.Dto;
using System;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Application.Services.Implementations
{
    public class VotePostService : IVotePostService
    {
        private readonly IServiceBusVotePostTopicSender<BirdVoteDto> serviceBusVotePostTopicSender;
        private readonly IVotePostRepository votePostRepository;

        public VotePostService(
            IServiceBusVotePostTopicSender<BirdVoteDto> serviceBusVotePostTopicSender,
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

            var message = new BirdVoteDto()
            {
                PostId = votePost.PostId,
                UserId = votePost.UserId,
                SpecieId = votePost.SpecieId
            };

            await this.serviceBusVotePostTopicSender.SendMessage(votePost, TopicHelper.LABEL_VOTE_CREATED);

            return result;
        }

        public async Task<bool> DeleteVotePost(Guid voteId, string partitionKey, string userId)
        {
            var vote = await this.votePostRepository.GetVoteAsync(voteId, partitionKey);
            if (userId == vote.UserId && vote != null)
            {
                var result = await this.votePostRepository.DeleteVoteAsync(voteId, partitionKey);
                if (result)
                {
                    await this.serviceBusVotePostTopicSender.SendMessage(vote, TopicHelper.LABEL_VOTE_DELETED);
                    return true;
                }

                return result;
            }
            else
            {
                throw new UnauthorizedRemove();
            }
        }
    }
}
