using System;
using System.Collections.Generic;

namespace FL.Web.API.Core.User.Posts.Domain.Dto
{
    public class VotePostResponse
    {
        public Guid PostId { get; set; }
        public string VoteId { get; set; }
    }
}
