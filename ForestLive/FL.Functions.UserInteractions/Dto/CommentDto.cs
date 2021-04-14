using Newtonsoft.Json;
using System;

namespace FL.Functions.UserInteractions.Dto
{
    public class CommentDto : CommentBaseDto
    {
        [JsonProperty(PropertyName = "titlePost")]
        public string TitlePost { get; set; }
    }
}
