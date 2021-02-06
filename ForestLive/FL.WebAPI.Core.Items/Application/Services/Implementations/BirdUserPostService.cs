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

        public BirdUserPostService(IBirdUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task CreateUserAsync(BirdUser user)
        {
            try
            {
                await this.userRepository.CreateUserAsync(user);
            }
            catch (Exception ex)
            {
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
            }

            return new List<BirdPost>();
        }
    }
}
