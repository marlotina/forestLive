using FL.WebAPI.Core.Users.Api.Models.v1.Response;
using FL.WebAPI.Core.Users.Domain.Entities;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Models.v1.Request;

namespace FL.WebAPI.Core.Users.Mappers.v1.Implementation
{
    public class UserLabelMapper : IUserLabelMapper
    {
        public UserLabel Convert(UserLabelRequest source)
        {
            var result = default(UserLabel);
            if (source != null)
            {
                result = new UserLabel()
                {
                    Id = source.Label.ToLower(),
                    UserId = source.UserId
                };
            }

            return result;
        }

        public UserLabelResponse Convert(UserLabel source)
        {
            var result = default(UserLabelResponse);
            if (source != null)
            {
                result = new UserLabelResponse()
                {
                    Label = source.Id,
                    PostCount = source.PostCount
                };
            }

            return result;
        }

        public UserLabelDetailsResponse ConvertDetails(UserLabel source)
        {
            var result = default(UserLabelDetailsResponse);
            if (source != null)
            {
                result = new UserLabelDetailsResponse()
                {
                    Label = source.Id,
                    UserId = source.UserId,
                    PostCount = source.PostCount,
                    CreationDate = source.CreationDate.ToString("dd/MM/yyyy")
                };
            }

            return result;
        }
    }
}
