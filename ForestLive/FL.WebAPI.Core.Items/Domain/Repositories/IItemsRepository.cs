using FL.WebAPI.Core.Items.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IItemsRepository
    {
        Task CreateItemAsync(Item comment);

        Task<Item> GetItemAsync(string postId);

        Task UpsertBlogPostAsync(Item post);

        Task CreateItemCommentAsync(ItemComment comment);

        Task<List<ItemComment>> GetItemCommentsAsync(string postId);
    }
}
