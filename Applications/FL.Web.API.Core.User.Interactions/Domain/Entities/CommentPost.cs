﻿using Newtonsoft.Json;
using System;

namespace FL.Web.API.Core.User.Interactions.Domain.Entities
{
    public class CommentPost
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "titlePost")]
        public string TitlePost { get; set; }
        
        [JsonProperty(PropertyName = "AuthorPostId")]
        public string AuthorPostId { get; set; }

        [JsonProperty(PropertyName = "specieId")]
        public Guid? SpecieId { get; set; }
    }
}
