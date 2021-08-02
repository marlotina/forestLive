using Newtonsoft.Json;

namespace FL.Web.API.Core.User.Interactions.Domain.Dto
{
    public class UserFollowerDto
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
}
