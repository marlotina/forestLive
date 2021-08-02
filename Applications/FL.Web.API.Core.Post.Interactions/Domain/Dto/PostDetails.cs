using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Domain.Dto
{
    public class PostDetails
    {
        //VotePost
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public DateTime CreationDate { get; set; }

        //voteComment
        [JsonProperty(PropertyName = "commentId")]
        public Guid CommentId { get; set; }

        //birdcomment
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "voteCount")]
        public int VoteCount { get; set; }

        [JsonProperty(PropertyName = "parentId")]
        public Guid? ParentId { get; set; }
    }
}
