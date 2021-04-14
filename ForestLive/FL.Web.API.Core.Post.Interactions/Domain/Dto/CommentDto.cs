using Newtonsoft.Json;
using System;

namespace FL.Web.API.Core.Post.Interactions.Domain.Dto
{
    public class CommentDto : CommentBaseDto
    {
        [JsonProperty(PropertyName = "titlePost")]
        public string TitlePost { get; set; }
    }
}
