using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IUserFollowRepository
    {
        Task<bool> GetFollow(string userId, string followUserId);
    }
}
