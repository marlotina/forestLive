using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Application.Services.Contracts
{
    public interface ICommentService
    {
        Task<BirdComment> AddComment(BirdComment commnet);

        Task<bool> DeleteComment(Guid commentId, Guid postId, string userId);

        Task<List<BirdComment>> GetCommentByPostId(Guid psotId);
    }
}
