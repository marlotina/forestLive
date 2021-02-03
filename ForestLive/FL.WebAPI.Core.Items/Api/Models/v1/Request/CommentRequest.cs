using System;

namespace FL.WebAPI.Core.Items.Models.v1.Request
{
    public class CommentRequest
    {
        public Guid PostId { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }
    }
}
