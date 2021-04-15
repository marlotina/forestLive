using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IUserManagedService
    {
        Task<Domain.Entities.User> GetByIdAsync(Guid userId);

        Task<bool> UpdateAsync(Domain.Entities.User user);

        Task<bool> DeleteAsync(Guid userId);

        Task<bool> UpdatePhotoAsync(Guid userId, string photo);
    }
}
