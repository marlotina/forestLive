using System;

namespace FL.WebAPI.Core.Searchs.Api.Models.v1.Response
{
    public class BirdMapResponse
    {
        public PositionResponse Location { get; set; }

        public Guid PostId { get; set; }

        public Guid SpecieId { get; set; }
    }
}
