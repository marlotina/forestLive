using FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Contracts;
using FL.Web.API.Core.User.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.User.Interactions.Domain.Entities;

namespace FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Implementation
{
    public class FollowMapper : IFollowMapper
    {

        public FollowUser Convert(FollowUserRequest source)
        {
            var result = default(FollowUser);
            if (source != null)
            {
                result = new FollowUser()
                {
                    FollowUserId = source.FollowUserId,
                    UserId = source.UserId
                };
            }

            return result;
        }

        public FollowUserResponse Convert(FollowUser source)
        {
            var result = default(FollowUserResponse);
            if (source != null)
            {
                result = new FollowUserResponse()
                {
                    FollowUserId = source.FollowUserId,
                    UserId = source.UserId
                };
            }

            return result;
        }
    }
}
