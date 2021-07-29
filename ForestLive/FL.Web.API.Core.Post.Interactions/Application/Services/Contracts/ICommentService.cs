using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using FL.Web.API.Core.Post.Interactions.Models.v1.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Application.Services.Contracts
{
    public interface ICommentService
    {
        Task<BirdComment> AddComment(CommentDto commnet);

        Task<bool> DeleteComment(Guid commentId, Guid postId, string userId);

        Task<IEnumerable<CommentResponse>> GetCommentByPost(Guid postId, string userId);
    }
}
