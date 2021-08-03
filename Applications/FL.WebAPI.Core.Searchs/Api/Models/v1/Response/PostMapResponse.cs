using System;

namespace FL.WebAPI.Core.Searchs.Api.Models.v1.Response
{
    public class PostMapResponse
    {
        public PositionResponse Location { get; set; }

        public Guid PostId { get; set; }

        public string UserId { get; set; }
    }
}
