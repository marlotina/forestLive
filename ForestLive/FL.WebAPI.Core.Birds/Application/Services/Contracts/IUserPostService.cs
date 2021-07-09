using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface IUserPostService
    {
        Task<IEnumerable<PostDto>> GetUserPosts(string userId, string label, string type, string langugeId);

        Task<IEnumerable<PointPostDto>> GetMapPointsByUserId(string userId);
    }
}     