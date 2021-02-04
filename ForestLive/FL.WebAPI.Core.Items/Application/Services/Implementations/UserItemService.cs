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
    public class UserItemService : IUserItemService
    {
        private readonly IUserRepository userRepository;

        public UserItemService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task CreateUserAsync(UserBird user)
        {
            try
            {
                await this.userRepository.CreateUserAsync(user);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<List<Item>> GetBlogPostsForUserId(string userId)
        {
            try 
            {
                var result = await this.userRepository.GetBlogPostsForUserId(userId);
                return result;
            }
            catch (Exception ex) 
            { 
            }

            return null;
        }
    }
}
