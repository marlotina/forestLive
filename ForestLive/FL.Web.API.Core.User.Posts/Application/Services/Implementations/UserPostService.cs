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
            try
            {
                var posts = await this.iUserRepository.GetMapPointsForUserIdAsync(userId);

                return posts;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }

        public async Task<BirdPost> GetPostByPostId(string postId, string userId)
        {
            try
            {
                var post = await this.iUserRepository.GetPostsByPostIdAsync(postId, userId);

                return post;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }

        public async Task<IEnumerable<PostDto>> GetUserPostByLabel(string label, string userId)
        {
            try
            {
                var posts = await this.iUserRepository.GetPostsByLabelByUserIdAsync(label, userId);

                return posts;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }

        public async Task<IEnumerable<PostDto>> GetPostsByUserId(string userId)
        {
            try 
            {
                var posts = await this.iUserRepository.GetPostsByUserIdAsync(userId);

                return posts;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }
    }
}
