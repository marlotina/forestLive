using FL.Web.Api.Core.Votes.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Application.Services.Contracts
{
    public interface IVotePostService
    {
        Task<VotePost> AddVotePost(VotePost votePost);

        Task<bool> DeleteVotePost(Guid voteId, string partitionKey, string userId);
    }
}
