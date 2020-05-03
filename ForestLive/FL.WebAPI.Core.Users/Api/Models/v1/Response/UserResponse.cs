using System;

namespace FL.WebAPI.Core.Users.Models.v1.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string UrlWebSite { get; set; }

        public DateTime LastModification { get; set; }

        public bool IsCompany { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid LanguageId { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public string Location { get; set; }
    }
}
