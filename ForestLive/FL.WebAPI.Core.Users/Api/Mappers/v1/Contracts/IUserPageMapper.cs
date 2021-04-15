using FL.WebAPI.Core.Users.Api.Models.v1.Response;
using FL.WebAPI.Core.Users.Models.v1.Response;

namespace FL.WebAPI.Core.Users.Api.Mappers.v1.Contracts
{
    public interface IUserPageMapper
    {
        UserPageResponse Convert(Domain.Entities.User source);

        UserListSiteResponse ConvertList(Domain.Entities.User source);
    }
}
