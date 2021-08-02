using Newtonsoft.Json;

namespace FL.Web.API.Core.User.Interactions.Domain.Entities
{
    public class FollowerUser : Follow
    {
        [JsonProperty(PropertyName = "followerUserId")]
        public string FollowerUserId { get; set; }
    }
}
