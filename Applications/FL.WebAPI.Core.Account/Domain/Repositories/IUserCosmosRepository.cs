using FL.WebAPI.Core.Account.Domain.Entities;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IUserCosmosRepository
    {
        Task<UserInfo> CreateUserInfoAsync(UserInfo user);

        Task<string> GetUserImage(string userId, string userNameId);
    }
}
