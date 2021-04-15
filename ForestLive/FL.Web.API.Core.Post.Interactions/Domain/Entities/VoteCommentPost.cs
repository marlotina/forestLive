﻿using Newtonsoft.Json;
using System;

namespace FL.Web.API.Core.Post.Interactions.Domain.Entities
{
    public class VoteCommentPost
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "commentId")]
        public Guid CommentId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public DateTime CreationDate { get; set; }
    }

}
