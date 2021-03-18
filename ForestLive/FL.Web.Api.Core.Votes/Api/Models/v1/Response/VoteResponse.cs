using System;

namespace FL.Web.Api.Core.Votes.Api.Models.v1.Response
{
    public class VoteResponse
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public string AuthorPostUserId { get; set; }

        public string UserId { get; set; }

        public string CreationDate { get; set; }

        public string TitlePost { get; set; }
    }
}
