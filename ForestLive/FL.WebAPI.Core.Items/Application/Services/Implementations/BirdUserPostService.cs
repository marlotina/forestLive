using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Entities.User;
using FL.WebAPI.Core.Items.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Implementations
{
    public class BirdUserPostService : IBirdUserPostService
    {
        private readonly IBirdUserRepository userRepository;
        private readonly ILogger<BirdUserPostService> logger;

        public BirdUserPostService(
            IBirdUserRepository userRepository,
            ILogger<BirdUserPostService> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task CreateUserAsync(BirdUser user)
        {
            try
            {
                await this.userRepository.CreateUserAsync(user);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "CreateUserAsync");
            }
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
