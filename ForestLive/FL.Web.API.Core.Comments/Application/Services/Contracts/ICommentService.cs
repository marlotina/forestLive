using FL.Web.API.Core.Comments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Comments.Application.Services.Contracts
{
    public interface ICommentService
    {
        Task<BirdComment> AddComment(BirdComment commnet);

        Task<List<BirdComment>> GetCommentByPost(string userId);

        Task<bool> DeleteComment(Guid commentId, Guid postId, string userId);
    }
}
