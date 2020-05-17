using Microsoft.AspNetCore.Identity;
using System;

namespace FL.WebAPI.Core.Users.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }

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

        public DateTime? LastLogin { get; set; }

        public DateTime RegistrationDate { get; set; }
        
        public bool AcceptedConditions { get; set; }

        public DateTime? AcceptedConditionsDate { get; set; }
    }
}
