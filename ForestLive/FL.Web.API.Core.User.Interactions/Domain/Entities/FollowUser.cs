using Newtonsoft.Json;
using System;

namespace FL.Web.API.Core.User.Interactions.Domain.Entities
{
    public class FollowUser
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "followUserId")]
        public string FollowUserId { get; set; }
    }
}
