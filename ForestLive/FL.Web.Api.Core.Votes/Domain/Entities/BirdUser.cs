using System;

namespace FL.Web.Api.Core.Votes.Domain.Entities.User
{
    public class BirdUser
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
