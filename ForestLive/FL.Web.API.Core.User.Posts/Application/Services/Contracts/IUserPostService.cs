using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Application.Services.Contracts
{
    public interface IUserPostService
    {
        Task<IEnumerable<PostDto>> GetPostsByUserId(string userId);

        Task<IEnumerable<PostDto>> GetPostsByLabelByUserId(string label, string userId);

        Task<BirdPost> GetPostByPostId(string postId, string userId);

        Task<IEnumerable<PointPostDto>> GetMapPointsByUserId(string userId);
    }
}
