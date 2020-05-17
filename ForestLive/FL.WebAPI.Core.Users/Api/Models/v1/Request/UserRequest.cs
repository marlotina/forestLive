using System;
using System.ComponentModel.DataAnnotations;

namespace FL.WebAPI.Core.Users.Models.v1.Request
{
    public class UserRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string UrlWebSite { get; set; }

        public bool IsCompany { get; set; }
        
        public Guid LanguageId { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public string Location { get; set; }

        public string LinkedlinUrl { get; set; }

        public string InstagramUrl { get; set; }

        public string TwitterUrl { get; set; }

        public string FacebookUrl { get; set; }
    }
}
