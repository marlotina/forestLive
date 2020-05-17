using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FL.WebAPI.Core.Users.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Entities.User> GetByIdAsync(Guid id);
        
        Task<IEnumerable<Entities.User>> FindByEmailAsync(string email);

        Task<bool> UpdateAsync(Entities.User user);

        Task<bool> DeleteAsync(Guid id);
        Task<Entities.User> GetByUserNameAsync(string userName);
    }
}