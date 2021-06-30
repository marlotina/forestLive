using System;

namespace FL.WebAPI.Core.Users.Api.Models.v1.Response
{
    public class UserPageResponse
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Id { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public string TwitterUrl { get; set; }

        public string InstagramUrl { get; set; }

        public string LinkedlinUrl { get; set; }

        public string FacebookUrl { get; set; }
        
        public string Location { get; set; }

        public string UrlWebSite { get; set; }

        public bool IsCompany { get; set; }

        public Guid UserSystemId { get; set; }
    }
}
