using System;
using System.Collections.Generic;

namespace FL.Web.API.Core.Post.Interactions.Domain.Dto
{
    public class VotePostRequest
    {
        public IEnumerable<Guid> ListComments { get; set; }

        public string UserId { get; set; }
    }
}
