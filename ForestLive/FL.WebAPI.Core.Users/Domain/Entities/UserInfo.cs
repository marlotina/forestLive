using System;

namespace FL.WebAPI.Core.Users.Domain.Entities
{
    public class UserInfo
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public Guid UserSystemId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Surname { get; set; }

        public string UrlWebSite { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public string Location { get; set; }

        public bool IsCompany { get; set; }

        public string LinkedlinUrl { get; set; }

        public string InstagramUrl { get; set; }

        public string TwitterUrl { get; set; }

        public string FacebookUrl { get; set; }

        public Guid LanguageId { get; set; }

        public DateTime LastModification { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int FollowerCount { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
