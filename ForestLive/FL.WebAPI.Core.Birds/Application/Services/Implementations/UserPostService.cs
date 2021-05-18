using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Implementations
{
    public class UserPostService : IUserPostService
    {
        private readonly IUserPostRepository iUserRepository;

        public UserPostService(
            IUserPostRepository iUserRepository)
        {
            this.iUserRepository = iUserRepository;
        }

        public async Task<IEnumerable<PointPostDto>> GetMapPointsByUserId(string userId)
        {
            return await this.iUserRepository.GetMapPointsByUserAsync(userId);
        }

        public Task<IEnumerable<PostDto>> GetUserPosts(string userId, string label, string type)
        {
            return this.iUserRepository.GetUserPosts(userId, label, type);
        }
    }
}
