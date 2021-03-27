using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Application.Services.Contracts
{
    public interface IVotePostService
    {
        Task<VotePost> AddVotePost(VotePost votePost);

        Task<bool> DeleteVotePost(Guid voteId, Guid postId, string userId);

        Task<IEnumerable<VotePost>> GetVoteByPost(Guid postId);
    }
}
