using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Domain.Repositories
{
    public interface IVotePostRepository
    {
        Task<VotePost> AddVote(VotePost votePost);

        Task<bool> DeleteVoteAsync(string id, Guid postId);

        Task<VotePost> GetVoteAsync(string voteId, Guid postId);

        Task<IEnumerable<VotePost>> GetVoteByPostAsync(Guid postId);
    }
}
