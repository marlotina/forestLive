using FL.Web.API.Core.User.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.User.Interactions.Domain.Entities;

namespace FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Contracts
{
    public interface IFollowMapper
    {
        FollowUser Convert(FollowUserRequest source);

        FollowUserResponse Convert(FollowUser source);
    }
}
