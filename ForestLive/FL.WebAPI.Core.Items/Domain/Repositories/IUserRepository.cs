using FL.WebAPI.Core.Items.Domain.Entities.User;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(UserBird user);
    }
}
