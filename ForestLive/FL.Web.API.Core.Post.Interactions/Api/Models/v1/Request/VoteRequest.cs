﻿using System;

namespace FL.Web.API.Core.Post.Interactions.Api.Models.v1.Request
{
    public class VoteRequest
    {
        public string UserId { get; set; }

        public Guid PostId { get; set; }

        public int Vote { get; set; }

        public string AuthorPostUserId { get; set; }

        public string TitlePost { get; set; }

        public Guid? SpecieId { get; set; }
    }
}
