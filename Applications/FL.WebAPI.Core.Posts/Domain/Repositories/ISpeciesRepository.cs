using FL.WebAPI.Core.Posts.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Posts.Domain.Repository
{
    public interface ISpeciesRepository
    {
        Task<BirdPost> GetPostsAsync(Guid postId, Guid specieId);

        Task<bool> CreatePostAsync(BirdPost post);

        Task<bool> DeletePostAsync(Guid postId, Guid specieId);
    }
}
