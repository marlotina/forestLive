﻿using System;

namespace FL.Web.API.Core.Comments.Models.v1.Request
{
    public class BirdCommentRequest
    {
        public Guid PostId { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }
    }
}
