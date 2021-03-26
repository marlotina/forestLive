using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Domain.Repositories
{
    public interface IBirdUserRepository
    {
        Task<List<PostDto>> GetPostsByUserIdAsync(string userId);

        Task<List<PointPostDto>> GetMapPointsForUserIdAsync(string userId);

        Task<BirdPost> GetPostsByPostIdAsync(string postId, string userId);

        Task<IEnumerable<PostDto>> GetPostsByLabelByUserIdAsync(string label, string userId);
    }
}
