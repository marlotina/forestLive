using System;

namespace FL.WebAPI.Core.Users.Models.v1.Response
{
    public class UserListSiteResponse
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string UrlWebSite { get; set; }

        public bool IsCompany { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid LanguageId { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public string Location { get; set; }

        public string LinkedlinUrl { get; set; }

        public string InstagramUrl { get; set; }

        public string TwitterUrl { get; set; }

        public string FacebookUrl { get; set; }

        public string FollowId { get; set; }

        public bool HasFollow { get; set; }

        public int FollowerCount { get; set; }
        
    }
}
