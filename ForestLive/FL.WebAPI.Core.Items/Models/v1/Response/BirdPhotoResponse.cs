using System;

namespace FL.WebAPI.Core.Items.Api.Models.v1.Response
{
    public class BirdPhotoResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }

        public string UserName { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string[] Labels { get; set; }

        public string BirdSpecie { get; set; }

        public Guid SpecieId { get; set; }

        public string ItemUrl { get; set; }

        public string UserUrl { get; set; }

        public DateTime ObservationDate { get; set; }
    }
}
