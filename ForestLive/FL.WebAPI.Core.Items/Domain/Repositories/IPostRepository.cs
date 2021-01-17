using FL.WebAPI.Core.Items.Domain.Entities;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IPostRepository
    {
        Task<BirdPost> AddBirdPost(BirdPost birdPost);
    }
}
