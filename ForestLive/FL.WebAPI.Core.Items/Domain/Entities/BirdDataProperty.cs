using System;
using System.Drawing;

namespace FL.WebAPI.Core.Items.Domain.Entities
{
    public class BirdDataProperty
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string SpecieName { get; set; }

        public string ImageUrl { get; set; }

        public string[] LabelsData { get; set; }

        public DateTime CreateDate { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }

        public string UserName { get; set; }

        public Guid UserId { get; set; }
    }
}