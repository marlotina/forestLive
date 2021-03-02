using Newtonsoft.Json;
using System;

namespace FL.Functions.UserPost.Model
{
    public class CommentPostDto
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "createDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
}
