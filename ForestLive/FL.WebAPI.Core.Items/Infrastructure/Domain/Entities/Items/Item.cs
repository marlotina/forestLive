using System;

namespace FL.WebAPI.Core.Items.Domain.Entities.Items
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string SpecieName { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string[] Labels { get; set; }

        public DateTime CreateDate { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }

        public string UserName { get; set; }

        public Guid UserId { get; set; }

    }
}
