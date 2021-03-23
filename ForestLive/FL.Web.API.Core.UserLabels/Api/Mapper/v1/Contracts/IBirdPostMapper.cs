using FL.Web.API.Core.User.Posts.Api.Models.v1.Request;
using FL.Web.API.Core.User.Posts.Api.Models.v1.Response;
using FL.Web.API.Core.User.Posts.Domain.Entities;
using FL.Web.API.Core.UserLabels.Api.Models.v1.Response;
using FL.Web.API.Core.UserLabels.Domain.Dto;

namespace FL.WebAPI.Core.UserLabels.Api.Mapper.v1.Contracts
{
    public interface IBirdPostMapper
    {
        UserLabel Convert(UserLabelRequest source);

        UserLabelDetailsResponse Convert(UserLabel source);

        UserLabelResponse Convert(UserLabelDto source);
        
    }
}
