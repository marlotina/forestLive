using Newtonsoft.Json;
using System;

namespace FL.Web.API.Core.Post.Interactions.Domain.Dto
{
    public class VotePostDto : VotePostBaseDto
    {
        [JsonProperty(PropertyName = "titlePost")]
        public string TitlePost { get; set; }
    }

}
