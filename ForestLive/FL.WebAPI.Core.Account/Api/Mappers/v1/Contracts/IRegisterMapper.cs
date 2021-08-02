using FL.WebAPI.Core.Account.Models.v1.Request;
using FL.WebAPI.Core.Account.Models.v1.Response;

namespace FL.WebAPI.Core.Account.Mappers.v1.Contracts
{
    public interface IRegisterMapper
    {
        Domain.Entities.User Convert(RegisterRequest source);

        RegisterResponse Convert(Domain.Entities.User source);
    }
}
