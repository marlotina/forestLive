using System;
using System.Collections.Generic;

namespace FL.Web.Api.Core.Votes.Api.Models.v1.Request
{
    public class VotePostRequest
    {
        public List<Guid> ListPosts { get; set; }

        public string UserId { get; set; }
    }
}
