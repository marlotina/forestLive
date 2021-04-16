using FL.WebAPI.Core.Users.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IUserManagedService
    {
        Task<bool> UpdateAsync(UserInfo newUserData, string userIdRequest);

        Task<bool> DeleteAsync(Guid userId, string userIdRequest);

        Task<UserInfo> GetUserAsync(string userId);

        Task<UserInfo> GetUserAsync(Guid userId, string userName);
        
    }
}
