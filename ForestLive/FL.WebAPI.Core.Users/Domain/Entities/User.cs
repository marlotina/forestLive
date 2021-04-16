using Microsoft.AspNetCore.Identity;
using System;

namespace FL.WebAPI.Core.Users.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime RegistrationDate { get; set; }
        
        public bool AcceptedConditions { get; set; }

        public DateTime? AcceptedConditionsDate { get; set; }
    }
}
