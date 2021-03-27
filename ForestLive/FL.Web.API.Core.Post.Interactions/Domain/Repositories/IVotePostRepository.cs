using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Domain.Repositories
{
    public interface IVotePostRepository
    {
        Task<VotePost> AddVote(VotePost votePost);

        Task<bool> DeleteVoteAsync(Guid id, Guid postId);

        Task<VotePost> GetVoteAsync(Guid voteId, Guid postId);

        Task<IEnumerable<VotePost>> GetVoteByPost(Guid postId);
    }
}
