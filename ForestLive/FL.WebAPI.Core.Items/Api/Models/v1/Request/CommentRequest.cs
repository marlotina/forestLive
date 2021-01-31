using System;

namespace FL.WebAPI.Core.Items.Models.v1.Request
{
    public class CommentRequest
    {
        public Guid PostId { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
