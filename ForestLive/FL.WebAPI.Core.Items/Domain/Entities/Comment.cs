﻿using System;

namespace FL.WebAPI.Core.Items.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public string Type { get; set; }

        public string Text { get; set; }

        public int LikesCount { get; set; }

        public DateTime CreateDate { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
