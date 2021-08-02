using Newtonsoft.Json;
using System;

namespace FL.Functions.UserInteractions.Dto
{
    public class VotePostBaseDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "authorPostId")]
        public string AuthorPostId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty(PropertyName = "specieId")]
        public Guid? SpecieId { get; set; }
    }

}
