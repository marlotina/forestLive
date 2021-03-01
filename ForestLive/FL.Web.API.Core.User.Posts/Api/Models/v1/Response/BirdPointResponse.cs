using FL.Web.API.Core.User.Posts.Api.Models.v1.Response;
using System;

namespace FL.WebAPI.Core.User.Posts.Api.Models.v1.Response
{
    public class BirdPointResponse
    {
        public string Title { get; set; }

        public string UserId { get; set; }

        public string ImageUrl { get; set; }

        public string AltImage { get; set; }

        public PositionResponse Location { get; set; }

        public string BirdSpecie { get; set; }

        public Guid SpecieId { get; set; }

        public Guid PostId { get; set; }
    }
}
