using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IBIrdPostRepository
    {
        Task CreatePostAsync(BirdPost comment);

        Task DeletePostAsync(Guid id, string partitionKey);

        Task UpdatePostAsync(BirdPost post);

        Task<BirdPost> GetPostAsync(Guid postId);

        Task CreateCommentAsync(BirdComment comment);

        Task DeleteCommentAsync(Guid commentId, Guid itemId);

        Task<List<BirdComment>> GetCommentsAsync(Guid itemId);
    }
}
