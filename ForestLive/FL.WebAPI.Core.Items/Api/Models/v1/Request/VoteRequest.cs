using System;

namespace FL.WebAPI.Core.Items.Api.Models.v1.Request
{
    public class VoteRequest
    {
        public string UserId { get; set; }

        public Guid PostId { get; set; }

        public string Title { get; set; }

        public int Vote { get; set; }
    }
}
