using FL.WebAPI.Core.Users.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IUserManagedService
    {
        Task<bool> UpdateAsync(UserInfo newUserData, string userIdRequest);

        Task<bool> DeleteAsync(string userId, string userIdRequest);

        Task<UserInfo> GetUserAsync(string userId);

        Task<UserInfo> GetUserAsync(string userSystemId, string userName);
        
    }
}
