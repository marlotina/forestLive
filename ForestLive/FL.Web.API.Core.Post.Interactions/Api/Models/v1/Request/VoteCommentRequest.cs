using System;

namespace FL.Web.API.Core.Post.Interactions.Api.Models.v1.Request
{
    public class VoteCommentRequest
    {
        public string UserId { get; set; }

        public Guid CommentId { get; set; }

        public string AuthorPostId { get; set; }

        public string TitlePost { get; set; }

        public Guid? SpecieId { get; set; }
    }
}
