using FL.Web.Api.Core.Votes.Api.Models.v1.Request;
using FL.Web.Api.Core.Votes.Api.Models.v1.Response;
using FL.Web.Api.Core.Votes.Domain.Entities;

namespace FL.Web.Api.Core.Votes.Api.Mapper.v1.Contracts
{
    public interface IVoteMapper
    {
        VotePost Convert(VoteRequest source);


        VoteResponse Convert(VotePost source);
    }
}
