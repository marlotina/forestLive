using FL.ServiceBus.Standard.Implementations;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Enum;
using FL.WebAPI.Core.Items.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Implementations
{
    public class VotePostService : IVotePostService
    {
        private readonly ServiceBusTopicSender<VotePost> serviceBusTopicSender;
        private readonly IVotePostRepository votePostRepository;

        public VotePostService(
            ServiceBusTopicSender<VotePost> serviceBusTopicSender,
            IVotePostRepository votePostRepository)
        {
            this.votePostRepository = votePostRepository;
            this.serviceBusTopicSender = serviceBusTopicSender;
        }

        public async Task<VotePost> AddVotePost(VotePost votePost)
        {

            votePost.CreationDate = DateTime.UtcNow;
            votePost.Type = ItemHelper.VOTE_TYPE;

            var result = await this.votePostRepository.AddVotePost(votePost);
            await this.serviceBusTopicSender.SendMessage(votePost);

            return result;

        }
    }
}
