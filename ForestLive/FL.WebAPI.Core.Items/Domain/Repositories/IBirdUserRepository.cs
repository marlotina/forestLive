using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IBirdUserRepository
    {
        Task<List<BirdPost>> GetBlogPostsForUserId(string userId);

        Task DeleteItemAsync(Guid id, string partitionKey);

    }
}
