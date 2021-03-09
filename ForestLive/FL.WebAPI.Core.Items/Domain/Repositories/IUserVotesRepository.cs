using FL.WebAPI.Core.Items.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IUserVotesRepository
    {
        Task<IEnumerable<VotePostResponse>> GetUserVoteByPosts(IEnumerable<Guid> listPosts, string userId);
    }
}
