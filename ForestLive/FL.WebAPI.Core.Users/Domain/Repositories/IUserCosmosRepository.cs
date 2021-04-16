using FL.WebAPI.Core.Users.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IUserCosmosRepository
    {
        Task<UserInfo> CreateUserInfoAsync(UserInfo user);

        Task<UserInfo> UpdateUserInfoAsync(UserInfo user);

        Task DeleteUserInfoAsync(string userId);

        Task<UserInfo> GetUser(string userId);
    }
}
