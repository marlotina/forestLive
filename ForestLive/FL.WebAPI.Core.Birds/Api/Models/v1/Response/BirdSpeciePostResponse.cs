using System;

namespace FL.WebAPI.Core.Birds.Api.Models.v1.Response
{
    public class BirdSpeciePostResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public string AltImage { get; set; }

        public DateTime CreateDate { get; set; }

        public string UserId { get; set; }

        public int VoteCount { get; set; }

        public int CommentCount { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string[] Labels { get; set; }

        public string BirdSpecie { get; set; }

        public Guid SpecieId { get; set; }

        public string UserPhoto { get; set; }

        public string ObservationDate { get; set; }

        public Guid PostId { get; set; }
    }
}
