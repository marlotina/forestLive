using System;

namespace FL.WebAPI.Core.Items.Models.v1.Response
{
    public class CommentResponse
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
