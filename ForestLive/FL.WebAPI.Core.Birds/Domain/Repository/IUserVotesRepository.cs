using FL.WebAPI.Core.Birds.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface IUserVotesRepository
    {
        Task<IEnumerable<VotePostResponse>> GetUserVoteByPosts(IEnumerable<Guid> listPosts, string userId);
    }
}
