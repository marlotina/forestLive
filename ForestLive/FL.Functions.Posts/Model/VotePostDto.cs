﻿using Newtonsoft.Json;
using System;

namespace FL.Functions.Posts.Model
{
    public class VotePostDto
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "PostId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "CreationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty(PropertyName = "Vote")]
        public int Vote { get; set; }
    }
}