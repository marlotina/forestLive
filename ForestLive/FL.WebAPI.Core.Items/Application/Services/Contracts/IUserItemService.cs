using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IUserItemService
    {
        Task CreateUserAsync(UserBird user);

        Task<List<Item>> GetBlogPostsForUserId(string userId);
    }
}
