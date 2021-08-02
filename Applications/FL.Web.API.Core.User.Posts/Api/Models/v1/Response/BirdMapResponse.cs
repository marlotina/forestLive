using FL.Web.API.Core.User.Posts.Api.Models.v1.Response;
using System;

namespace FL.WebAPI.Core.User.Posts.Api.Models.v1.Response
{
    public class BirdMapResponse
    {
        public PositionResponse Location { get; set; }

        public Guid PostId { get; set; }
    }
}
