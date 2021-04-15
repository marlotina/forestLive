using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Entities.User> GetByUserNameAsync(string userName);

        Task<IEnumerable<Entities.User>> GetUsersAsync();
    }
}