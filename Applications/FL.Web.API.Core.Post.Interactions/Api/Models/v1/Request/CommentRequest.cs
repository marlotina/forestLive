﻿using System;

namespace FL.Web.API.Core.Post.Interactions.Models.v1.Request
{
    public class CommentRequest
    {
        public Guid PostId { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public Guid? SpecieId { get; set; }

        public string TitlePost { get; set; }

        public string AuthorPostId { get; set; }

        public string ImagePost { get; set; }

        public Guid? ParentId { get; set; }
    }
}
