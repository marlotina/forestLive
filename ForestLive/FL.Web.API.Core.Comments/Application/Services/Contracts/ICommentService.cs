using FL.Web.API.Core.Comments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Comments.Application.Services.Contracts
{
    public interface ICommentService
    {
        Task<BirdComment> AddComment(BirdComment commnet, Guid specieId);

        Task<List<BirdComment>> GetCommentByUserId(string userId);

        Task<bool> DeleteComment(Guid commentId, string userId, Guid specieId);
    }
}
