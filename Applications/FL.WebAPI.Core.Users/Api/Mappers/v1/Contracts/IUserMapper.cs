using FL.WebAPI.Core.Users.Domain.Entities;
using FL.WebAPI.Core.Users.Models.v1.Request;
using FL.WebAPI.Core.Users.Models.v1.Response;

namespace FL.WebAPI.Core.Users.Mappers.v1.Contracts
{
    public interface IUserMapper
    {
        UserInfo Convert(UserRequest source);

        UserResponse Convert(UserInfo source);

        UserInfoResponse ConvertUserInfo(User source);
    }
}
