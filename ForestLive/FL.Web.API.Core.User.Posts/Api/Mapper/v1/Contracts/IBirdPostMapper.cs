using FL.WebAPI.Core.User.Posts.Api.Models.v1.Response;
using FL.WebAPI.Core.User.Posts.Domain.Entities;

namespace FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Contracts
{
    public interface IBirdPostMapper
    {
        BirdPostResponse Convert(BirdPost source);

        BirdPointResponse MapConvert(BirdPost source);
    }
}
