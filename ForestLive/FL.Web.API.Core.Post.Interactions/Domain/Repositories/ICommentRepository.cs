using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task<BirdComment> CreateCommentAsync(BirdComment comment);

        Task<bool> DeleteCommentAsync(BirdComment comment);

        Task<List<PostDetails>> GetCommentsByPostIdAsync(Guid postId, string userId);

        Task<BirdComment> GetCommentAsync(Guid commentId, Guid postId);

        Task<bool> IncreaseVoteCommentCountAsync(Guid commentId, Guid postId);

        Task<bool> DecreaseVoteCommentCountAsync(Guid commentId, Guid postId);
    }
}
