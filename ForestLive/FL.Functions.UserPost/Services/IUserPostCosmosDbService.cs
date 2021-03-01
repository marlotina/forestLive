using FL.Functions.UserPost.Model;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public interface IUserPostCosmosDbService
    {
        Task CreatePostInPendingAsync(BirdPostDto post);

        Task DeletePostInPendingAsync(BirdPostDto post);
    }
}
