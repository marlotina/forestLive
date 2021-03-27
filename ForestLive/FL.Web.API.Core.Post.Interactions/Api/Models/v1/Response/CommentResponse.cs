using System;

namespace FL.Web.API.Core.Post.Interactions.Models.v1.Response
{
    public class CommentResponse
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public string Text { get; set; }

        public string CreationDate { get; set; }

        public string UserId { get; set; }

        public string UserImage { get; set; }

        public string TitlePost { get; set; }

        public string AuthorPostUserId { get; set; }

        public Guid? SpecieId { get; set; }
    }
}
