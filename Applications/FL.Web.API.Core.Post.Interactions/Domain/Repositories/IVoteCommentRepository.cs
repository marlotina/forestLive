using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Domain.Repositories
{
    public interface IVoteCommentRepository
    {

        Task<VoteCommentPost> AddCommentVoteAsync(VoteCommentPost voteCommentPost);

        Task<bool> DeleteCommentVoteAsync(string voteId, Guid postId);

        Task<VoteCommentPost> GetVoteAsync(string voteId, Guid postId);
    }
}
