using FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Contracts;
using FL.Web.API.Core.User.Interactions.Api.Models.v1.Response;
using FL.Web.API.Core.User.Interactions.Domain.Entities;

namespace FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Implementation
{
    public class VoteMapper : IVoteMapper
    {
        public VoteResponse Convert(VotePost source)
        {
            var result = default(VoteResponse);
            if (source != null)
            {
                result = new VoteResponse()
                {
                    TitlePost = source.TitlePost,
                    UserId = source.UserId,
                    PostId = source.PostId,
                    CreationDate = source.CreationDate.ToString("ddMMyyyhhmm"),
                    Id = source.Id,
                    AuthorPostUserId = source.AuthorPostUserId,
                    SpecieId = source.SpecieId
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
