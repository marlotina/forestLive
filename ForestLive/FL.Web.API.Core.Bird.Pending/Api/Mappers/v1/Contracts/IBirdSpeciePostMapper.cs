using FL.Web.API.Core.Bird.Pending.Api.Models.v1.Request;
using FL.Web.API.Core.Bird.Pending.Api.Models.v1.Response;
using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using FL.Web.API.Core.Bird.Pending.Domain.Model;
using System.Collections.Generic;

namespace FL.Web.API.Core.Bird.Pending.Api.Mappers.v1.Contracts
{
    public interface IBirdSpeciePostMapper
    {
        BirdPostResponse Convert(PostDto source, IEnumerable<VotePostResponse> postVotes = null);

        BirdMapResponse MapConvert(BirdPost source);

        BirdPost Convert(PostRequest source);

        PostResponse ConvertPost(BirdPost source, IEnumerable<VotePostResponse> postVotes = null);

        BirdPostResponse Convert(BirdPost source);

        ModalBirdPostResponse ModalConvert(BirdPost source);
    }
}
