using System;

namespace FL.WebAPI.Core.Users.Models.v1.Response
{
    public class AuthResponse
    {
        public string Token { get; set; }

        public DateTime ExpiretionDate { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public bool HasPhoto { get; set; }
    }
}
