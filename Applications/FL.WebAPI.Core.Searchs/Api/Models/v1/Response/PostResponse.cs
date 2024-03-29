﻿using System;

namespace FL.WebAPI.Core.Searchs.Api.Models.v1.Response
{
    public class PostResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public bool IsPost { get; set; }

        public string ImageUrl { get; set; }

        public string AltImage { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserId { get; set; }

        public int VoteCount { get; set; }

        public int CommentCount { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string[] Labels { get; set; }

        public string BirdSpecie { get; set; }

        public Guid? SpecieId { get; set; }

        public string UserUrl { get; set; }

        public string ObservationDate { get; set; }

        public string UserPhoto { get; set; }

        public Guid PostId { get; set; }

        public bool HasVote { get; set; }

        public string VoteId { get; set; }

        public string SpecieUrl { get; set; }
    }
}
