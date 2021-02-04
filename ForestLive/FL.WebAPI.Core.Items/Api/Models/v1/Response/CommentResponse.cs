using System;

namespace FL.WebAPI.Core.Items.Models.v1.Response
{
    public class CommentResponse
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public string Text { get; set; }

        public DateTime CreateDate { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
