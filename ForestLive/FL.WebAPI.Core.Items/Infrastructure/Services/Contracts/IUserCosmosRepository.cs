using FL.WebAPI.Core.Items.Domain.Entities.User;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Services.Contracts
{
    public interface IUserCosmosRepository
    {
        Task CreateUserAsync(UserBird user);
    }
}
