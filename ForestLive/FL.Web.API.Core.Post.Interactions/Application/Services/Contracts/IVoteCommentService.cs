using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Application.Services.Contracts
{
    public interface IVoteCommentService
    {
        Task<VoteCommentPost> AddVoteCommentPost(VoteCommentPostDto votePost);

        Task<bool> DeleteVoteComment(string voteId, Guid postId, string userId);
    }
}
