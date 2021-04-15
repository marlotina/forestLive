using System;
using System.Collections.Generic;

namespace FL.Web.API.Core.User.Interactions.Api.Models.v1.Request
{
    public class VoteCommentRequest
    {
        public List<Guid> ListComments { get; set; }

        public string UserId { get; set; }
    }
}
