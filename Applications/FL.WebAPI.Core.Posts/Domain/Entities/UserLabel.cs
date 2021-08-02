using System;

namespace FL.WebAPI.Core.Posts.Domain.Entities
{
    public class UserLabel
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string UserId { get; set; }

        public int PostCount { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
