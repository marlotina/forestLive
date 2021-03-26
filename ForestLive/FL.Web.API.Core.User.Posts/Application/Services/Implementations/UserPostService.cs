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
        private readonly IBirdUserRepository userRepository;
        private readonly ILogger<UserPostService> logger;
        private readonly IUserVotesRepository userVotesRepository;

        public UserPostService(
            IBirdUserRepository userRepository,
            IUserVotesRepository userVotesRepository,
            ILogger<UserPostService> logger)
        {
            this.userVotesRepository = userVotesRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<PointPostDto>> GetMapPointsByUserId(string userId)
        {
            try
            {
                var posts = await this.userRepository.GetMapPointsForUserId(userId);

                return posts;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }

        public async Task<BirdPost> GetPostByPostId(string postId, string userId)
        {
            try
            {
                var post = await this.userRepository.GetPostsByPostId(postId, userId);

                return post;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }

        public async Task<IEnumerable<PostDto>> GetPostsByLabelByUserId(string label, string userId)
        {
            try
            {
                var posts = await this.userRepository.GetPostsByLabelByUserId(label, userId);

                return posts;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }

        public async Task<IEnumerable<PostDto>> GetPostsByUserId(string userId)
        {
            try 
            {
                var posts = await this.userRepository.GetPostsByUserId(userId);

                return posts;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }
    }
}
