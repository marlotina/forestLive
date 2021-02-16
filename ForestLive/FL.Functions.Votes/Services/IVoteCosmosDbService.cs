using FL.Functions.Votes.Model;
using System.Threading.Tasks;

namespace FL.Functions.Votes.Services
{
    public interface IVoteCosmosDbService
    {
        Task CreateVoteInUserAsync(VotePostDto post);
    }
}
