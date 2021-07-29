using FL.WebAPI.Core.Searchs.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Application.Services.Contracts
{
    public interface IUserVoteService
    {
        Task<IEnumerable<VotePostResponse>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId);
    }
}
