using Newtonsoft.Json;

namespace FL.Functions.UserInteractions.Dto
{
    public class UserFollowDto
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }

}