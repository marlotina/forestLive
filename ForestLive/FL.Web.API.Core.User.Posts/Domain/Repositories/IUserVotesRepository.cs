using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Domain.Repositories
{
    public interface IUserVotesRepository
    {
        Task<List<Guid>> GetUserVoteByPosts(List<Guid> listPosts, string userId);
    }
}
