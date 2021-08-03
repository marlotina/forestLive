using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Searchs.Api.Models.v1.Response;
using FL.WebAPI.Core.Searchs.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Model;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Searchs.Api.Mappers.v1.Contracts
{
    public interface IPostMapper
    {
        PostListResponse Convert(PostDto source, IEnumerable<VotePostResponse> postVotes = null);

        PostMapResponse Convert(PointPostDto source);

        PostResponse ConvertPost(BirdPost source, IEnumerable<VotePostResponse> postVotes = null);

        ModalPostResponse ModalConvert(BirdPost source);
    }
}
