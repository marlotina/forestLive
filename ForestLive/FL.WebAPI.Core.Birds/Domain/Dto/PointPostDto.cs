using Microsoft.Azure.Cosmos.Spatial;
using Newtonsoft.Json;
using System;

namespace FL.Web.API.Core.User.Posts.Domain.Dto
{
    public class PointPostDto
    {
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "location")]
        public Point Location { get; set; }
    }
}
