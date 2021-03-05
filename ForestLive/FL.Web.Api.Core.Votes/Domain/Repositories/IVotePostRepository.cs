using FL.Web.Api.Core.Votes.Domain.Entities;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Domain.Repositories
{
    public interface IVotePostRepository
    {
        Task<VotePost> AddVotePost(VotePost votePost);
    }
}
