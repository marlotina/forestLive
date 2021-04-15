using System.Threading.Tasks;
using System;

namespace FL.WebAPI.Core.Users.Domain.Repositories
{
    public interface IUserManagedRepository
    {
        Task<Entities.User> GetByIdAsync(Guid id);

        Task<bool> UpdateAsync(Entities.User user);

        Task<bool> DeleteAsync(Guid id);

        Task<Entities.User> GetByUserNameAsync(string userName);
    }
}