using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Domain.Repository
{
    public interface IUserVotesRestRepository
    {
        Task<IEnumerable<VotePostResponse>> GetUserVoteByPosts(IEnumerable<Guid> listPosts, string userId);
    }
}
