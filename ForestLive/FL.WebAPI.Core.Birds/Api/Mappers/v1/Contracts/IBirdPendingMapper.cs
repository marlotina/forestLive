using FL.Web.API.Core.Bird.Pending.Api.Models.v1.Request;
using FL.Web.API.Core.Bird.Pending.Api.Models.v1.Response;
using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using FL.Web.API.Core.Bird.Pending.Domain.Model;
using System.Collections.Generic;

namespace FL.Web.API.Core.Bird.Pending.Api.Mappers.v1.Contracts
{
    public interface IBirdPendingMapper
    {
        PostListResponse Convert(PostDto source, IEnumerable<VotePostResponse> postVotes = null);

        BirdPost Convert(PostRequest source);

        PostResponse Convert(BirdPost source, IEnumerable<VotePostResponse> postVotes = null);

        PostListResponse Convert(BirdPost source);
    }
}
