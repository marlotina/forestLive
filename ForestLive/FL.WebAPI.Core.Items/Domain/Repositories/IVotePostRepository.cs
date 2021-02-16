using FL.WebAPI.Core.Items.Domain.Entities;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IVotePostRepository
    {
        Task<VotePost> AddVotePost(VotePost votePost);
    }
}
