using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Application.Services.Contracts
{
    public interface IBirdPendingService
    {
        Task<List<PostDto>> GetBirdBySpecie(int orderBy);

        Task<IEnumerable<VotePostResponse>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId);

        Task<List<PostDto>> GetBirds(int orderBy);
    }
}
