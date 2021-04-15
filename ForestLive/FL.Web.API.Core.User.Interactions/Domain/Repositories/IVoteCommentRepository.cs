using FL.Web.API.Core.User.Interactions.Domain.Dto;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Domain.Repositories
{
    public interface IVoteCommentRepository
    {
        Task<List<VoteInfoDto>> GetVoteCommentsAsync(List<Guid> listComment, string userId);

        Task<List<VotePost>> GetVotesByUserId(string userId);
    }
}
