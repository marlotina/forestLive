using FL.Web.API.Core.Post.Interactions.Models.v1.Response;
using System.Collections.Generic;

namespace FL.Web.API.Core.Post.Interactions.Api.Models.v1.Response
{
    public class PostDataResponse
    {
        public string VotePostId { get; set; }

        public bool HasPostVote { get; set; }

        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}
