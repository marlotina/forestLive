using System;

namespace User.WebApi.Core.Integration.Tets.v1.Model.Requests
{
    public class UserRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Surname { get; set; }

        public string UrlWebSite { get; set; }

        public bool IsCompany { get; set; }

        public bool AcceptedConditions { get; set; }

        public Guid LanguageId { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }

        public bool HasPhoto { get; set; }

        public Guid? CityId { get; set; }

        public Guid? CountryId { get; set; }

        public string Email { get; set; }

        //public List<SubscriptionRequest> Subscriptions { get; set; }
    }
}
