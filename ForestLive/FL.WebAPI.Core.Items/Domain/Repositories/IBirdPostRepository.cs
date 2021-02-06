using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IBIrdPostRepository
    {
        Task CreateItemAsync(BirdPost comment);

        Task DeleteItemAsync(Guid id, string partitionKey);

        Task UpsertBlogPostAsync(BirdPost post);

        Task<BirdPost> GetItemAsync(Guid postId);


        Task CreateItemCommentAsync(BirdComment comment);

        Task DeleteCommentAsync(Guid commentId, Guid itemId);

        Task<List<BirdComment>> GetItemCommentsAsync(Guid itemId);
    }
}
