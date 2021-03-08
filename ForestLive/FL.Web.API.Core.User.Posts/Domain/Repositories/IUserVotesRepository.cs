using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Domain.Repositories
{
    public interface IUserVotesRepository
    {
        Task<IEnumerable<Guid>> GetUserVoteByPosts(IEnumerable<Guid> listPosts, string userId);
    }
}
