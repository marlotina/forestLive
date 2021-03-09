using FL.Web.Api.Core.Votes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Application.Services.Contracts
{
    public interface IVotePostService
    {
        Task<List<VotePost>> GetVoteUserByPost(List<Guid> listPost, string userId);

        Task<VotePost> AddVotePost(VotePost votePost, Guid specieId);

        Task<bool> DeleteVotePost(Guid voteId, string partitionKey, string userId);
    }
}
