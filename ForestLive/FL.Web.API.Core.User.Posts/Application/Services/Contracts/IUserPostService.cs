using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Application.Services.Contracts
{
    public interface IUserPostService
    {
        Task<IEnumerable<BirdPost>> GetPostsByUserId(string userId);

        Task<IEnumerable<BirdPost>> GetPostsByLabelByUserId(string label, string userId);

        Task<BirdPost> GetPostByPostId(string postId, string userId);
    }
}
