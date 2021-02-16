using FL.WebAPI.Core.Items.Domain.Entities;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IVotePostService
    {
        Task<VotePost> AddVotePost(VotePost votePost);
    }
}
