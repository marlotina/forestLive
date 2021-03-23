using FL.Web.API.Core.User.Posts.Api.Models.v1.Request;
using FL.Web.API.Core.User.Posts.Api.Models.v1.Response;
using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.Web.API.Core.User.Posts.Domain.Entities;
using FL.WebAPI.Core.User.Posts.Api.Models.v1.Response;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System.Collections.Generic;

namespace FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Contracts
{
    public interface IBirdPostMapper
    {
        BirdPostResponse Convert(BirdPost source, IEnumerable<VotePostResponse> postVotes);

        BirdMapResponse MapConvert(BirdPost source);

        ModalBirdPostResponse ModalConvert(BirdPost source);

        UserLabel Convert(UserLabelRequest source);

        UserLabelResponse Convert(UserLabel source);
    }
}
