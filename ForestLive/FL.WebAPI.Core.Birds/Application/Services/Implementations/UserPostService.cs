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
        private readonly IUserInfoService iUserInfoService;

        public UserPostService(
            IUserInfoService iUserInfoService,
            IUserPostRepository iUserRepository)
        {
            this.iUserRepository = iUserRepository;
            this.iUserInfoService = iUserInfoService;
        }

        public async Task<IEnumerable<PointPostDto>> GetMapPointsByUserId(string userId)
        {
            return await this.iUserRepository.GetMapPointsByUserAsync(userId);
        }

        public async Task<IEnumerable<PostDto>> GetUserPosts(string userId, string label, string type)
        {
            var result = await this.iUserRepository.GetUserPosts(userId, label, type);

            foreach (var post in result)
            {
                post.UserImage = await this.iUserInfoService.GetUserImageById(post.UserId);
            }

            return result;
        }
    }
}
