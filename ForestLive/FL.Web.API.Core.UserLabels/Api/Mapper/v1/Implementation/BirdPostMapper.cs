using FL.Web.API.Core.User.Posts.Domain.Entities;
using FL.Web.API.Core.UserLabels.Api.Models.v1.Response;
using FL.Web.API.Core.UserLabels.Domain.Dto;
using FL.WebAPI.Core.UserLabels.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.UserLabels.Api.Models.v1.Request;
using FL.WebAPI.Core.UserLabels.Api.Models.v1.Response;

namespace FL.WebAPI.Core.UserLabels.Api.Mapper.v1.Implementation
{
    public class BirdPostMapper : IBirdPostMapper
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

        public UserLabelResponse Convert(UserLabelDto source)
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

        public UserLabelDetailsResponse Convert(UserLabel source)
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
