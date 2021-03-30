using FL.Web.API.Core.User.Interactions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Domain.Repositories
{
    public interface IVotePostRepository
    {
        Task<List<VotePost>> GetVotePostAsync(List<Guid> listPost, string userId);

        Task<List<VotePost>> GetVotesByUserId(string userId);
    }
}
