﻿using FL.Pereza.Helpers.Standard.Enums;
using FL.Web.Api.Core.Votes.Application.Services.Contracts;
using FL.Web.Api.Core.Votes.Domain.Entities;
using FL.Web.Api.Core.Votes.Domain.Enum;
using FL.Web.Api.Core.Votes.Domain.Repositories;
using FL.Web.Api.Core.Votes.Infrastructure.ServiceBus.Contracts;
using System;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Application.Services.Implementations
{
    public class VotePostService : IVotePostService
    {
        private readonly IServiceBusVotePostTopicSender<VotePost> serviceBusVotePostTopicSender;
        private readonly IVotePostRepository votePostRepository;
        private readonly IBirdPostRepository birdPostRepository;

        public VotePostService(
            IServiceBusVotePostTopicSender<VotePost> serviceBusVotePostTopicSender,
            IVotePostRepository votePostRepository,
            IBirdPostRepository birdPostRepository)
        {
            this.votePostRepository = votePostRepository;
            this.serviceBusVotePostTopicSender = serviceBusVotePostTopicSender;
            this.birdPostRepository = birdPostRepository;
        }

        public async Task<VotePost> AddVotePost(VotePost votePost)
        {

            votePost.CreationDate = DateTime.UtcNow;
            votePost.Type = ItemHelper.VOTE_TYPE;

            var result = await this.votePostRepository.AddVotePost(votePost);
            await this.serviceBusVotePostTopicSender.SendMessage(votePost, TopicHelper.LABEL_VOTE_CREATED);

            var post = await this.birdPostRepository.GetPostAsync(votePost.PostId);
            if (post.VoteCount > 5)
            { 
                //remove the post in the pending container.
                //add the message in the subscription to save the post in birds.
            }

            return result;
        }
    }
}
