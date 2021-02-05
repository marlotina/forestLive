using FL.WebAPI.Core.Users.Models.v1.Request;
using FL.WebAPI.Core.Users.Models.v1.Response;

namespace FL.WebAPI.Core.Users.Mappers.v1.Contracts
{
    public interface IUserMapper
    {
        Domain.Entities.User Convert(UserRequest source);

        UserResponse Convert(Domain.Entities.User source);

        UserInfoResponse ConvertUserInfo(Domain.Entities.User source);
    }
}
