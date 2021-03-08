using Newtonsoft.Json;
using System;

namespace FL.Functions.BirdPost.Dto
{
    public class VotePostDto
    {
        [JsonProperty(PropertyName = "PostId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "SpecieId")]
        public Guid SpecieId { get; set; }   
    }
}
