using System;

namespace FL.WebAPI.Core.Posts.Api.Models.v1.Request
{
    public class PostRequest
    {
        public string ImageData { get; set; }

        public string AltImage { get; set; }

        public string Type { get; set; }

        public string ImageName { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string SpecieName { get; set; }

        public Guid? SpecieId { get; set; }

        public string CountryCode { get; set; }

        public string[] Labels { get; set; }

        public DateTime? ObservationDate { get; set; }

        public bool isPost { get; set; }
    }
}
