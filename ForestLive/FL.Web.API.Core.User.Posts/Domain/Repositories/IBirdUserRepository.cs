using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Domain.Repositories
{
    public interface IBirdUserRepository
    {
        Task<List<BirdPost>> GetPostsByUserId(string userId);

        Task<List<BirdPost>> GetMapPointsForUserId(string userId);

        Task<BirdPost> GetPostsByPostId(string postId, string userId);
    }
}
