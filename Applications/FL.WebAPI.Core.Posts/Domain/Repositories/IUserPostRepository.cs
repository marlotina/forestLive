using FL.WebAPI.Core.Posts.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Posts.Domain.Repository
{
    public interface IUserPostRepository
    {
        Task<bool> CreatePostAsync(BirdPost post);

        Task<bool> DeletePostAsync(Guid postId, string userId);

        Task<bool> UpdatePostAsync(BirdPost post, string userId);

        Task<BirdPost> GetPostAsync(Guid postId, string userId);
    }
}
