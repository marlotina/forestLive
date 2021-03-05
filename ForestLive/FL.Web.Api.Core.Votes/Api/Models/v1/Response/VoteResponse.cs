using System;

namespace FL.Web.Api.Core.Votes.Api.Models.v1.Response
{
    public class VoteResponse
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Guid OwnerUserId { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public int Vote { get; set; }
    }
}
