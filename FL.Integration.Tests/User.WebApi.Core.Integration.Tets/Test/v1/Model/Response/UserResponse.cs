using System;
using System.Collections.Generic;
using System.Text;

namespace User.WebApi.Core.Integration.Tets.v1.Model.Response
{
    public class UserResponse
    {

        public string Email { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime CreationDate { get; set; }

        public string UrlWebSite { get; set; }

        public DateTime LastModification { get; set; }

        public DateTime? LastLogin { get; set; }

        public bool IsCompany { get; set; }

        public bool AcceptedConditions { get; set; }

        public DateTime? AcceptedConditionsDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid LanguageId { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }

        public bool HasPhoto { get; set; }

        public Guid? CityId { get; set; }

        public Guid? CountryId { get; set; }
        public string UserName { get; set; }

        //public List<Subscription> Subscriptions { get; set; }
    }
}