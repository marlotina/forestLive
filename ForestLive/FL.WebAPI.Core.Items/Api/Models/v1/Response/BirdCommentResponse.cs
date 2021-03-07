﻿using System;

namespace FL.WebAPI.Core.Items.Api.Models.v1.Response
{
    public class BirdCommentResponse
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public string Text { get; set; }

        public string CreateDate { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }
    }
}
