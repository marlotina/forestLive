using FL.Pereza.Helpers.Standard.Enums;
using FL.Web.Api.Core.Votes.Application.Exceptions;
using FL.Web.Api.Core.Votes.Application.Services.Contracts;
using FL.Web.Api.Core.Votes.Domain.Entities;
using FL.Web.Api.Core.Votes.Domain.Enum;
using FL.Web.Api.Core.Votes.Domain.Repositories;
using FL.Web.Api.Core.Votes.Infrastructure.ServiceBus.Contracts;
using FL.Web.API.Core.Votes.Domain.Dto;
using System;
using System.Collections.Generic;
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

        public async Task<VotePost> AddVotePost(VotePost votePost, Guid specieId)
        {
            votePost.Id = Guid.NewGuid();
            votePost.CreationDate = DateTime.UtcNow;
            votePost.Type = ItemHelper.VOTE_TYPE;

            var result = await this.votePostRepository.AddVote(votePost);

            var message = this.Convert(votePost, specieId);
            await this.serviceBusVotePostTopicSender.SendMessage(message, TopicHelper.LABEL_VOTE_CREATED);

            return result;
        }

        public async Task<bool> DeleteVotePost(Guid voteId, string partitionKey, string userId, Guid specieId)
        {
            var vote = await this.votePostRepository.GetVoteAsync(voteId, partitionKey);
            if (userId == vote.UserId && vote != null)
            {
                var result = await this.votePostRepository.DeleteVoteAsync(voteId, partitionKey);
                if (result)
                {
                    var message = this.Convert(vote, specieId);
                    await this.serviceBusVotePostTopicSender.SendMessage(message, TopicHelper.LABEL_VOTE_DELETED);
                    return true;
                }

                return result;
            }
            else
            {
                throw new UnauthorizedRemove();
            }
        }

        public async Task<List<VotePost>> GetVoteUserByPost(List<Guid> listPost, string userId)
        {
            return await this.votePostRepository.GetVotePostAsync(listPost, userId);
        }

        private BirdVoteDto Convert(VotePost source, Guid specieId)
        {
            var result = default(BirdVoteDto);
            if (source != null)
            {
                result = new BirdVoteDto()
                {
                    Title = source.Title,
                    UserId = source.UserId,
                    PostId = source.PostId,
                    CreationDate = source.CreationDate,
                    Id = source.Id,
                    OwnerUserId = source.OwnerUserId,
                    SpecieId = specieId,
                    Type = source.Type
                };
            }

            return result;
        }
    }
}
