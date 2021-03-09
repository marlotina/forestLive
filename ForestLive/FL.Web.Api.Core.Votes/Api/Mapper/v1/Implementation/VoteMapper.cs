using FL.Web.Api.Core.Votes.Api.Mapper.v1.Contracts;
using FL.Web.Api.Core.Votes.Api.Models.v1.Request;
using FL.Web.Api.Core.Votes.Api.Models.v1.Response;
using FL.Web.Api.Core.Votes.Domain.Entities;

namespace FL.Web.Api.Core.Votes.Api.Mapper.v1.Implementation
{
    public class VoteMapper : IVoteMapper
    {
        public VotePost Convert(VoteRequest source)
        {
            var result = default(VotePost);
            if (source != null)
            {
                result = new VotePost()
                {
                    Title = source.Title,
                    UserId = source.UserId,
                    PostId = source.PostId,
                    OwnerUserId = source.OwnerUserId
                };
            }
            return result;
        }

        public VoteResponse Convert(VotePost source)
        {
            var result = default(VoteResponse);
            if (source != null)
            {
                result = new VoteResponse()
                {
                    Title = source.Title,
                    UserId = source.UserId,
                    PostId = source.PostId,
                    CreationDate = source.CreationDate,
                    Id = source.Id,
                    OwnerUserId = source.OwnerUserId
                };
            }
            return result;
        }

        public VotePostResponse ConvertUserVote(VotePost source)
        {
            var result = default(VotePostResponse);
            if (source != null)
            {
                result = new VotePostResponse()
                {
                    VoteId = source.Id,
                    PostId = source.PostId,
                };
            }
            return result;
        }
    }
}
