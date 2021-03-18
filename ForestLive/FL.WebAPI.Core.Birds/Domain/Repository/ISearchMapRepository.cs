using FL.WebAPI.Core.Birds.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface ISearchMapRepository
    {
        Task<List<BirdPost>> GetPostByRadio(double latitude, double longitude, int meters);

        Task<BirdPost> GetPostsByPostId(string postId, string userId);
    }
}
