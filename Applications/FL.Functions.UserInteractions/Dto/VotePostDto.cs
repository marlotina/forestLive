using Newtonsoft.Json;
using System;

namespace FL.Functions.UserInteractions.Dto
{
    public class VotePostDto : VotePostBaseDto
    {
        [JsonProperty(PropertyName = "titlePost")]
        public string TitlePost { get; set; }
    }

}
