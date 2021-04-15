using System;
using System.Collections.Generic;

namespace FL.Web.API.Core.Post.Interactions.Models.v1.Response
{
    public class CommentResponse
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string CreationDate { get; set; }

        public string UserId { get; set; }

        public Guid? ParentId { get; set; }

        public int VoteCount { get; set; }

        public bool HasVote { get; set; }

        public string VoteId { get; set; }

        public List<CommentResponse> Replies { get; set; }

        public string UserImage { get; set; }
    }
}