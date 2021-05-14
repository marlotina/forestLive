using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Birds.Api.Models.v1.Response;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts
{
    public interface IPostMapper
    {
        PostListResponse Convert(PostDto source, IEnumerable<VotePostResponse> postVotes = null);

        BirdMapResponse Convert(PointPostDto source);

        PostResponse ConvertPost(BirdPost source, IEnumerable<VotePostResponse> postVotes = null);

        ModalBirdPostResponse ModalConvert(BirdPost source);

        PostHomeResponse Convert(PostHomeDto source);
    }
}
