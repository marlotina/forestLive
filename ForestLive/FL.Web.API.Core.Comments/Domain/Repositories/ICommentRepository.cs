using FL.Web.API.Core.Comments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Comments.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task<BirdComment> CreateCommentAsync(BirdComment comment);

        Task<bool> DeleteCommentAsync(Guid commentId, string userId);

        Task<List<BirdComment>> GetCommentsAsync(string userId);

        Task<BirdComment> GetCommentAsync(Guid commentId, string userId);
    }
}
