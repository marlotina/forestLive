using System;

namespace FL.WebAPI.Core.Items.Domain.Entities
{
    public class CommentProperty
    {
        public string UserName { get; set; }

        public string Text { get; set; }

        public int LikesCount { get; set; }

        public DateTime CreateDate { get; set; }
    }
}