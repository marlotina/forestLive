using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.Web.API.Core.User.Posts.Domain.Repositories;
using FL.WebAPI.Core.User.Posts.Application.Services.Contracts;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using FL.WebAPI.Core.User.Posts.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Application.Services.Implementations
{
    public class UserPostService : IUserPostService
    {
        private readonly IBirdUserRepository iUserRepository;
        private readonly ILogger<UserPostService> iLogger;

        public UserPostService(
            IBirdUserRepository iUserRepository,
            ILogger<UserPostService> iLogger)
        {
            this.iUserRepository = iUserRepository;
            this.iLogger = iLogger;
        }

        public async Task<IEnumerable<PointPostDto>> GetMapPointsByUserId(string userId)
        {
            return await this.iUserRepository.GetMapPointsByUserAsync(userId);
        }



        public async Task<BirdPost> GetPostByPostId(Guid postId, string userId)
        {
            return await this.iUserRepository.GetPostsAsync(postId, userId);
        }

        public Task<IEnumerable<PostDto>> GetUserPosts(string userId, string label, string type)
        {
            return this.iUserRepository.GetUserPosts(userId, label, type);
        }
    }
}
