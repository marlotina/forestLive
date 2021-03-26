using FL.Pereza.Helpers.Standard.Enums;
using FL.Web.Api.Core.Votes.Application.Exceptions;
using FL.Web.Api.Core.Votes.Application.Services.Contracts;
using FL.Web.Api.Core.Votes.Domain.Dto;
using FL.Web.Api.Core.Votes.Domain.Entities;
using FL.Web.Api.Core.Votes.Domain.Enum;
using FL.Web.Api.Core.Votes.Domain.Repositories;
using FL.Web.Api.Core.Votes.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Application.Services.Implementations
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

        public async Task<bool> DeleteVotePost(Guid voteId, string userId)
        {
            var vote = await this.votePostRepository.GetVoteAsync(voteId, userId);
            if (userId == vote.UserId && vote != null)
            {
                var result = await this.votePostRepository.DeleteVoteAsync(voteId, userId);
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

        public async Task<List<VotePost>> GetVotesByUserId(string userId)
        {
            return await this.votePostRepository.GetVotesByUserId(userId);
        }

        public async Task<List<VotePost>> GetVoteUserByPost(List<Guid> listPost, string userId)
        {
            return await this.votePostRepository.GetVotePostAsync(listPost, userId);
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
