using System;

namespace FL.Web.API.Core.Post.Interactions.Models.v1.Request
{
    public class BirdCommentRequest
    {
        public Guid PostId { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public Guid? SpecieId { get; set; }

        public string TitlePost { get; set; }

        public string AuthorPostUserId { get; set; }
    }
}
