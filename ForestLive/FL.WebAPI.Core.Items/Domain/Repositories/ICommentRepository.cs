using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> AddComment(Comment comment);

        Task<bool> DeleteComment(Guid idComment);
    }
}
