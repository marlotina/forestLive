using System;

namespace FL.WebAPI.Core.Birds.Domain.Dto
{
    public class VotePostResponse
    {
        public Guid PostId { get; set; }
        public string VoteId { get; set; }
    }
}
