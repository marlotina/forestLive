using FL.WebAPI.Core.Users.Api.Models.v1.Response;
using FL.WebAPI.Core.Users.Domain.Entities;
using FL.WebAPI.Core.Users.Models.v1.Request;

namespace FL.WebAPI.Core.Users.Mappers.v1.Contracts
{
    public interface IUserLabelMapper
    {
        UserLabel Convert(UserLabelRequest source);

        UserLabelDetailsResponse ConvertDetails(UserLabel source);

        UserLabelResponse Convert(UserLabel source);
        
    }
}
