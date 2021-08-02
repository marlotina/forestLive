using Newtonsoft.Json;

namespace FL.Web.API.Core.User.Interactions.Domain.Entities
{
    public class FollowUser : Follow
    {
        [JsonProperty(PropertyName = "followUserId")]
        public string FollowUserId { get; set; }
    }
}
