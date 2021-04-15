using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Domain.Repositories
{
    public interface IUserVotesRestRepository
    {
        Task<IEnumerable<VotePostResponse>> GetUserVoteByComments(IEnumerable<Guid> listComments, string userId);
    }
}
