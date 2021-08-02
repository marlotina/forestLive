using System;
using System.Collections.Generic;

namespace FL.Web.API.Core.Bird.Pending.Domain.Dto
{
    public class VotePostRequest
    {
        public IEnumerable<Guid> ListPosts { get; set; }

        public string UserId { get; set; }
    }
}
