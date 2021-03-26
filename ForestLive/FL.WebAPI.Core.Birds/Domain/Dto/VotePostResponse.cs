using System;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Birds.Domain.Dto
{
    public class VotePostResponse
    {
        public Guid PostId { get; set; }
        public Guid VoteId { get; set; }
    }
}
