using System;

namespace FL.WebAPI.Core.Items.Api.Models.v1.Response
{
    public class ItemResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreateDate { get; set; }

        public string UserId { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string[] Labels { get; set; }

        public string BirdSpecie { get; set; }

        public Guid SpecieId { get; set; }

        public string UserUrl { get; set; }

        public DateTime ObservationDate { get; set; }

        public bool SpecieConfirmed { get; set; }
    }
}
