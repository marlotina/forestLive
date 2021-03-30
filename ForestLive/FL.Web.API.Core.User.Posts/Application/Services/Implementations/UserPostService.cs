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
                var posts = await this.iUserRepository.GetMapPointsByUserAsync(userId);

                return posts;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }



        public async Task<BirdPost> GetPostByPostId(Guid postId, string userId)
        {
            try
            {
                var post = await this.iUserRepository.GetPostsAsync(postId, userId);

                return post;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }

        public async Task<IEnumerable<PostDto>> GetUserPost(string label, string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(label) && label != "null") {
                    return await this.iUserRepository.GetPostsByLabelAsync(label, userId);
                }
                
                return await this.iUserRepository.GetPostsByUserAsync(userId);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }

        public async Task<IEnumerable<PostDto>> GetUserBirds(string userId, Guid? specieId)
        {
            try 
            {
                if(specieId.HasValue)
                    return await this.iUserRepository.GetBirdsBySpecieAsync(userId, specieId.Value);

                return await this.iUserRepository.GetAllBirdsAsync(userId);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }

        public async Task<List<PostDto>> GetAllByUserAsync(string userId)
        {
            return await this.iUserRepository.GetAllByUserAsync(userId);
        }
    }
}
