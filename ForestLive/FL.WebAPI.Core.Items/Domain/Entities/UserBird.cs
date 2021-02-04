using System;

namespace FL.WebAPI.Core.Items.Domain.Entities.User
{
    public class UserBird
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Type { get; set; }

        public string Photo { get; set; }

        public string Name { get; set; }

        public int FollowCount { get; set; }

        public int FollowersCount { get; set; }
    }
}
