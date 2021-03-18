using System;
using System.Collections.Generic;

namespace FL.Web.API.Core.User.Posts.Domain.Dto
{
    public class VotePostRequest
    {
        public IEnumerable<Guid> ListPosts { get; set; }

        public string UserId { get; set; }
    }
}
