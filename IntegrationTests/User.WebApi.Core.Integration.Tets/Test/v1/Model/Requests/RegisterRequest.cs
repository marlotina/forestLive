using System;

namespace User.WebApi.Core.Integration.Tets.v1.Model.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public Guid LanguageId { get; set; }

        public string UserName { get; set; }
    }
}
