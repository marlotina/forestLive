using FL.Pereza.Helpers.Standard.Enums;
using System;

namespace FL.WebAPI.Core.Items.Api.Models.v1.Response
{
    public class BirdPostResponse
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

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string[] Labels { get; set; }

        public string BirdSpecie { get; set; }

        public Guid SpecieId { get; set; }

        public string UserUrl { get; set; }

        public string ObservationDate { get; set; }

        public Guid PostId { get; set; }
    }
}
