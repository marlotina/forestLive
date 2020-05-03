using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<Domain.Entities.User>> FindByEmailAsync(string email);

        Task<Domain.Entities.User> GetByIdAsync(Guid userId);

        Task<bool> UpdateAsync(Domain.Entities.User user);

        Task<bool> DeleteAsync(Guid userId);
    }
}
