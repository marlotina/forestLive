using System;

namespace FL.WebAPI.Core.Birds.Api.Models.v1.Response
{
    public class BirdMapResponse
    {
        public PositionResponse Location { get; set; }

        public Guid PostId { get; set; }
    }
}
