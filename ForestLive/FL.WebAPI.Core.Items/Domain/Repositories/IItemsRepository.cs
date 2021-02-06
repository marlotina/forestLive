using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IItemsRepository
    {
        Task CreateItemAsync(Item comment);

        Task DeleteItemAsync(Guid id, string partitionKey);

        Task UpsertBlogPostAsync(Item post);

        Task<Item> GetItemAsync(Guid postId);


        Task CreateItemCommentAsync(ItemComment comment);

        Task DeleteCommentAsync(Guid commentId, Guid itemId);

        Task<List<ItemComment>> GetItemCommentsAsync(Guid itemId);
    }
}
