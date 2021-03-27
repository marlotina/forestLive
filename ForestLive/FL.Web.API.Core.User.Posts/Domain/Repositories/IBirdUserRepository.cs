using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Domain.Repositories
{
    public interface IBirdUserRepository
    {
        Task<List<PostDto>> GetPostsByUserAsync(string userId);

        Task<List<PostDto>> GetBirdsBySpecieAsync(string userId, Guid? specieId);

        Task<List<PostDto>> GetAllBirdsAsync(string userId);

        Task<List<PointPostDto>> GetMapPointsByUserAsync(string userId);

        Task<BirdPost> GetPostsAsync(Guid postId, string userId);

        Task<IEnumerable<PostDto>> GetPostsByLabelAsync(string label, string userId);
    }
}
