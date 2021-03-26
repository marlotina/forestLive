using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Domain.Repositories
{
    public interface IBirdUserRepository
    {
        Task<List<PostDto>> GetPostsByUserId(string userId);

        Task<List<PointPostDto>> GetMapPointsForUserId(string userId);

        Task<BirdPost> GetPostsByPostId(string postId, string userId);

        Task<IEnumerable<PostDto>> GetPostsByLabelByUserId(string label, string userId);
    }
}
