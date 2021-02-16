using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Domain.Entities;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation
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
                    Vote = source.Vote
                };
            }
            return result;
        }
    }
}
