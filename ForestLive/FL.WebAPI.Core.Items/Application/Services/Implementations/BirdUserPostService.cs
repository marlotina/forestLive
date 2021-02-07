using FL.Logging.Implementation.Standard;
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
        //private readonly Logger<BirdUserPostService> logger;

        public BirdUserPostService(
            IBirdUserRepository userRepository)
            //Logger<BirdUserPostService> logger)
        {
            this.userRepository = userRepository;
            //this.logger = logger;
        }

        public async Task CreateUserAsync(BirdUser user)
        {
            try
            {
                await this.userRepository.CreateUserAsync(user);
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex, "CreateUserAsync");
            }
        }

        public async Task<List<BirdPost>> GetBlogPostsForUserId(string userId)
        {
            try 
            {
                var result = await this.userRepository.GetBlogPostsForUserId(userId);
                return result;
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex, "GetBlogPostsForUserId");
            }

            return new List<BirdPost>();
        }
    }
}
