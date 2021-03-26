using System;

namespace FL.WebAPI.Core.User.Posts.Api.Models.v1.Response
{
    public class PostListResponse
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public string AltImage { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserId { get; set; }

        public int VoteCount { get; set; }

        public int CommentCount { get; set; }

        public string[] Labels { get; set; }

        public string BirdSpecie { get; set; }

        public Guid? SpecieId { get; set; }

        public string UserUrl { get; set; }

        public string UserPhoto { get; set; }

        public Guid PostId { get; set; }

        public bool HasVote { get; set; }

        public Guid? VoteId { get; set; }
    }
}
