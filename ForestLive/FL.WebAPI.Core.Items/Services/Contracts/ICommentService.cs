using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Services.Contracts
{
    public interface ICommentService
    {
        Task<Comment> AddComment(Comment commnet);

        Task<bool> DeleteComment(Guid commnetId);
    }
}
