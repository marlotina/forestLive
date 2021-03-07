using FL.LogTrace.Contracts.Standard;
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

        public UserPostService(
            IBirdUserRepository userRepository,
            ILogger<UserPostService> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<BirdPost>> GetPostsByUserId(string userId)
        {
            try 
            {
                return await this.userRepository.GetBlogPostsForUserId(userId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }
    }
}
