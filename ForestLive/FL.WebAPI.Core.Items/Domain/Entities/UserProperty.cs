namespace FL.WebAPI.Core.Items.Domain.Entities.User
{
    public class UserProperty
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Photo { get; set; }

        public string Name { get; set; }

        public int FollowCount { get; set; }

        public int FollowersCount { get; set; }
    }
}