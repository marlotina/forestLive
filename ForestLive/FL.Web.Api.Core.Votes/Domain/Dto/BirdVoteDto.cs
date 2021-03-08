using Newtonsoft.Json;
using System;

namespace FL.Web.API.Core.Votes.Domain.Dto
{
    public class BirdVoteDto
    {
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "specieId")]
        public Guid SpecieId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
}
