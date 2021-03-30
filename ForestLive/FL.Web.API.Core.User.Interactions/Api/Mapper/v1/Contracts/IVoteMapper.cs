using FL.Web.API.Core.User.Interactions.Api.Models.v1.Response;
using FL.Web.API.Core.User.Interactions.Domain.Entities;

namespace FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Contracts
{
    public interface IVoteMapper
    {
        VoteResponse Convert(VotePost source);

        VotePostResponse ConvertUserVote(VotePost source);
    }
}
