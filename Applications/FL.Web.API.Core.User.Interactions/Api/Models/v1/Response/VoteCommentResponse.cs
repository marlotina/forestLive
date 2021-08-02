using System;

namespace FL.Web.API.Core.User.Interactions.Api.Models.v1.Response
{
    public class VoteCommentResponse
    {
        public string Id { get; set; }

        public Guid PostId { get; set; }

        public string AuthorPostId { get; set; }

        public string UserId { get; set; }

        public string CreationDate { get; set; }

        public string Text { get; set; }

        public Guid CommentId { get; set; }
    }
}
