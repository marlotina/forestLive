using System;

namespace FL.WebAPI.Core.Items.Api.Models.v1.Request
{
    public class ItemRequest
    {
        public string ImageData { get; set; }

        public string AltImage { get; set; }

        public string ImageName { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string SpecieName { get; set; }

        public Guid SpecieId { get; set; }

        public string[] Labels { get; set; }

        public DateTime ObservationDate { get; set; }
    }
}
