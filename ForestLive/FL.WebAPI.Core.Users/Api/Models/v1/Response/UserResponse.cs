﻿using System;

namespace FL.WebAPI.Core.Users.Models.v1.Response
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

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

        public string LinkedlinUrl { get; set; }

        public string InstagramUrl { get; set; }

        public string TwitterUrl { get; set; }

        public string FacebookUrl { get; set; }
    }
}
