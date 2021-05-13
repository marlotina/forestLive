using FL.Web.API.Core.Bird.Pending.Api.Models.v1.Response;
using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using System.Collections.Generic;

namespace FL.Web.API.Core.Bird.Pending.Api.Mappers.v1.Contracts
{
    public interface IBirdPendingMapper
    {
        PostListResponse Convert(PostDto source, IEnumerable<VotePostResponse> postVotes = null);
    }
}
