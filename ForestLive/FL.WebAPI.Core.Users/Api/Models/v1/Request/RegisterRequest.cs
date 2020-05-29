using System;
using System.ComponentModel.DataAnnotations;

namespace FL.WebAPI.Core.Users.Models.v1.Request
{
    public class RegisterRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

        public string LanguageCode { get; set; }
    }
}
