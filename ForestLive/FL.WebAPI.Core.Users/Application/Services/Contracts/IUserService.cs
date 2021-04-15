using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IUserService
    {
        Task<Domain.Entities.User> GetByUserNameAsync(string userName);

        Task<IEnumerable<Domain.Entities.User>> GetUsersAsync();
    }
}
