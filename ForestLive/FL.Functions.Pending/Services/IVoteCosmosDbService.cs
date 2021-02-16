using FL.Functions.Pending.Model;
using System.Threading.Tasks;

namespace FL.Functions.Pending.Services
{
    public interface IVoteCosmosDbService
    {
        Task CreateVoteInUserAsync(VotePostDto post);
    }
}
