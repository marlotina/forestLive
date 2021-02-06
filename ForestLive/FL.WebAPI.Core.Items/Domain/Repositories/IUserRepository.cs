using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(UserBird user);

        Task<List<Item>> GetBlogPostsForUserId(string userId);

        Task CreateItemAsync(Item item);

        Task DeleteItemAsync(Guid id, string partitionKey);

    }
}
