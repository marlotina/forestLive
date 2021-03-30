using FL.Web.API.Core.User.Interactions.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task<List<CommentPost>> GetCommentsByUserIdAsync(string userId);
    }
}
