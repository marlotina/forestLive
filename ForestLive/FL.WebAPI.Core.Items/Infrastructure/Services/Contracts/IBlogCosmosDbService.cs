using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Entities.User;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Services.Contracts
{
   public interface IBlogCosmosDbService
    {
        Task CreateItemAsync(Item comment);

        Task<Item> GetItemAsync(string postId);

        Task UpsertBlogPostAsync(Item post);

        Task CreateItemCommentAsync(ItemComment comment);

        Task<List<ItemComment>> GetItemCommentsAsync(string postId);

        Task CreateUserAsync(UserBird user);

        //Task UpdateUsernameAsync(BlogUser userWithUpdatedUsername, string oldUsername);

        //Task<BlogUser> GetUserAsync(string username);
    }
}
