using FL.Functions.Pending.Model;
using System.Threading.Tasks;

namespace FL.Functions.Pending.Services
{
    public interface IPendingCosmosDbService
    {
        Task CreatePostInPendingAsync(BirdPostDto post);

        Task DeletePostInPendingAsync(BirdPostDto post);
    }
}
