using System;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Searchs.Domain.Dto
{
    public class VotePostRequest
    {
        public IEnumerable<Guid> ListPosts { get; set; }

        public string UserId { get; set; }
    }
}
