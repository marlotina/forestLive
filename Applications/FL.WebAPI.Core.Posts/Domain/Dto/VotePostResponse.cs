using System;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Posts.Domain.Dto
{
    public class VotePostResponse
    {
        public Guid PostId { get; set; }
        public string VoteId { get; set; }
    }
}
