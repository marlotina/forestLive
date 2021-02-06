﻿using System;
using Newtonsoft.Json;

namespace FL.WebAPI.Core.Items.Domain.Entities
{
    public class BirdPostLike
    {
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get
            {
                return LikeId;
            }
        }

        [JsonProperty(PropertyName = "likeId")]
        public string LikeId { get; set; }


        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get
            {
                return "like";
            }
        }

        [JsonProperty(PropertyName = "postId")]
        public string PostId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string LikeAuthorId { get; set; }

        [JsonProperty(PropertyName = "userUsername")]
        public string LikeAuthorUsername { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime LikeDateCreated { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
