using Newtonsoft.Json;
using System;

namespace FL.Web.Api.Core.Votes.Domain.Entities
{
    public class VotePost
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "authorPostUserId")]
        public string AuthorPostUserId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public DateTime CreateionDate { get; set; }
    }

}
