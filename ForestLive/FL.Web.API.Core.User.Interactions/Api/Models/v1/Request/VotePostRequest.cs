using System;
using System.Collections.Generic;

namespace FL.Web.API.Core.User.Interactions.Api.Models.v1.Request
{
    public class VotePostRequest
    {
        public List<Guid> ListPosts { get; set; }

        public string UserId { get; set; }
    }
}
