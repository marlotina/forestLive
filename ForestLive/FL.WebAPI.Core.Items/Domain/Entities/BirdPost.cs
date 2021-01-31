using Microsoft.Azure.Cosmos.Spatial;
using System;

namespace FL.WebAPI.Core.Items.Domain.Entities
{
    public class BirdPost
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public Point Location { get; set; }

        public string SpecieName { get; set; }

        public Guid SpecieId { get; set; }

        public string ImageUrl { get; set; }

        public string AltImage { get; set; }

        public string[] Labels { get; set; }

        public DateTime CreateDate { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }

        public string UserName { get; set; }

        public Guid UserId { get; set; }

        public DateTime ObservationDate { get; set; }
    }
}
