using FL.Web.Api.Core.Votes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Domain.Repositories
{
    public interface IVotePostRepository
    {
        Task<VotePost> AddVote(VotePost votePost);

        Task<bool> DeleteVoteAsync(Guid id, string partitionKey);

        Task<VotePost> GetVoteAsync(Guid voteId, string userId);

        Task<List<VotePost>> GetVotePostAsync(List<Guid> listPost, string userId);

        Task<List<VotePost>> GetVotesByUserId(string userId);
    }
}
