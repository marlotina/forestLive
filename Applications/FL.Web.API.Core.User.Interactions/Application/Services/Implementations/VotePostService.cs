using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Implementations
{
    public class VotePostService : IVotePostService
    {
        private readonly IVotePostRepository votePostRepository;

        public VotePostService(
            IVotePostRepository votePostRepository)
        {
            this.votePostRepository = votePostRepository;
        }

        public async Task<List<VotePost>> GetVotesByUserId(string userId)
        {
            return await this.votePostRepository.GetVotesByUserId(userId);
        }

        public async Task<List<VotePost>> GetVoteUserByPost(List<Guid> listPost, string userId)
        {
            return await this.votePostRepository.GetVotePostAsync(listPost, userId);
        }
    }
}
