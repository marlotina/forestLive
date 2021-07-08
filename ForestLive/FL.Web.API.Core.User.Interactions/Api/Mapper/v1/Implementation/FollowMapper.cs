using FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Contracts;
using FL.Web.API.Core.User.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.User.Interactions.Api.Models.v1.Response;
using FL.Web.API.Core.User.Interactions.Domain.Entities;

namespace FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Implementation
{
    public class FollowMapper : IFollowMapper
    {

        public FollowUser Convert(FollowerUserRequest source)
        {
            var result = default(FollowUser);
            if (source != null)
            {
                result = new FollowUser()
                {
                    Id = $"{source.UserId}Follow{source.FollowUserId}",
                    UserId = source.UserId,
                    FollowUserId = source.FollowUserId
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
                    FollowerId = source.Id
                };
            }

            return result;
        }

        public FollowListResponse ConvertList(FollowUser source)
        {
            var result = default(FollowListResponse);
            if (source != null)
            {
                result = new FollowListResponse()
                {
                    FollowUserId = source.FollowUserId,
                    UserId = source.UserId,
                    Id = source.Id,
                    CreationDate = source.CreationDate.ToString("dd/MM/yyyy")
                };
            }

            return result;
        }

        public FollowListResponse ConvertList(FollowerUser source)
        {
            var result = default(FollowListResponse);
            if (source != null)
            {
                result = new FollowListResponse()
                {
                    FollowUserId = source.FollowerUserId,
                    UserId = source.UserId,
                    Id = source.Id,
                    CreationDate = source.CreationDate.ToString("dd/MM/yyyy")
                };
            }

            return result;
        }
    }
}
