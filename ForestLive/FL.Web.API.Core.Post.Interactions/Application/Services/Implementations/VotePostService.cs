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
        private readonly IServiceBusVotePostTopicSender<VotePostDto> iServiceBusVotePostTopicSender;
        private readonly IVotePostRepository iVotePostRepository;

        public VotePostService(
            IServiceBusVotePostTopicSender<VotePostDto> iServiceBusVotePostTopicSender,
            IVotePostRepository iVotePostRepository)
        {
            this.iVotePostRepository = iVotePostRepository;
            this.iServiceBusVotePostTopicSender = iServiceBusVotePostTopicSender;
        }

        public async Task<VotePost> AddVotePost(VotePost votePost)
        {
            votePost.Id = Guid.NewGuid();
            votePost.CreationDate = DateTime.UtcNow;
            votePost.Type = ItemHelper.VOTE_TYPE;

            var result = await this.iVotePostRepository.AddVote(votePost);
            var voteDto = this.Convert(result);
            await this.iServiceBusVotePostTopicSender.SendMessage(voteDto, TopicHelper.LABEL_VOTE_CREATED);

            return result;
        }

        public async Task<bool> DeleteVotePost(Guid voteId, Guid postId, string userId)
        {
            var vote = await this.iVotePostRepository.GetVoteAsync(voteId, postId);
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
                    AuthorPostId = source.AuthorPostId,
                    TitlePost = source.TitlePost
                };
            }
            return result;
        }
    }
}
