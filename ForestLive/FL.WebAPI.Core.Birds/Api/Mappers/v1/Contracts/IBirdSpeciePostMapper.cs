using FL.WebAPI.Core.Birds.Api.Models.v1.Request;
using FL.WebAPI.Core.Birds.Api.Models.v1.Response;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts
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
