using FL.Web.API.Core.Post.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.Post.Interactions.Api.Models.v1.Response;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;

namespace FL.Web.API.Core.Post.Interactions.Api.Mapper.v1.Contracts
{
    public interface IVoteMapper
    {
        VotePostDto Convert(VoteRequest source);


        VoteResponse Convert(VotePost source);

        VotePostResponse ConvertUserVote(VotePost source);
    }
}
