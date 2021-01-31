using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Services.Contracts
{
    public interface ICommentService
    {
        Task<ItemComment> AddComment(ItemComment commnet);

        Task<bool> DeleteComment(Guid commnetId);
    }
}
