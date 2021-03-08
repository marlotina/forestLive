using System;

namespace FL.Web.Api.Core.Votes.Api.Models.v1.Request
{
    public class VoteRequest
    {
        public string UserId { get; set; }

        public Guid PostId { get; set; }

        public string Title { get; set; }

        public int Vote { get; set; }

        public Guid SpecieId { get; set; }

        public string OwnerUserId { get; set; }
    }
}
