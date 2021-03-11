using Newtonsoft.Json;
using System;

namespace FL.Web.API.Core.Comments.Domain.Entities
{
    public class BirdComment
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

        [JsonProperty(PropertyName = "ttlePost")]
        public string TitlePost { get; set; }
        
        [JsonProperty(PropertyName = "authorPostUserId")]
        public string AuthorPostUserId { get; set; }
    }
}
