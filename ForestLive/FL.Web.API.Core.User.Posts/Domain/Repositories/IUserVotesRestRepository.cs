using FL.Web.API.Core.User.Posts.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Domain.Repositories
{
    public interface IUserVotesRestRepository
    {
        Task<IEnumerable<VotePostResponse>> GetUserVoteByPosts(IEnumerable<Guid> listPosts, string userId);
    }
}
