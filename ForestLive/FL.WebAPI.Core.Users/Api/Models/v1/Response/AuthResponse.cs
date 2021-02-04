﻿using System;

namespace FL.WebAPI.Core.Users.Models.v1.Response
{
    public class AuthResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Photo { get; internal set; }

        public string Token { get; set; }
    }
}
