using System;

namespace FL.WebAPI.Core.Birds.Api.Models.v1.Response
{
    public class PostHomeResponse
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string AltImage { get; set; }

        public string UserId { get; set; }

        public string BirdSpecie { get; set; }

        public Guid? SpecieId { get; set; }

        public string UserUrl { get; set; }

        public string UserPhoto { get; set; }

        public Guid PostId { get; set; }

        public string ObservationDate { get; set; }
    }
}
