using FL.WebAPI.Core.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IUserCosmosRepository
    {
        Task<UserInfo> CreateUserInfoAsync(UserInfo user);

        Task<bool> UpdateUserInfoAsync(UserInfo user);

        Task<bool> DeleteUserInfoAsync(string userId, string userNameId);

        Task<UserInfo> GetUser(string userId, string userNameId);


        Task<UserInfo> GetUserByName(string userNameId);

        Task<IEnumerable<UserInfo>> GetUsersAsync();
    }
}
