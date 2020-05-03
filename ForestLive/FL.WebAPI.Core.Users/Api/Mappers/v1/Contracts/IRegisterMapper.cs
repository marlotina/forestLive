using FL.WebAPI.Core.Users.Models.v1.Request;
using FL.WebAPI.Core.Users.Models.v1.Response;

namespace FL.WebAPI.Core.Users.Mappers.v1.Contracts
{
    public interface IRegisterMapper
    {
        Domain.Entities.User Convert(RegisterRequest source);

        RegisterResponse Convert(Domain.Entities.User source);
    }
}
