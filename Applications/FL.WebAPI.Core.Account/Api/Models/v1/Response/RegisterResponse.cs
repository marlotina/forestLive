﻿using System;

namespace FL.WebAPI.Core.Account.Models.v1.Response
{
    public class RegisterResponse
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
    }
}
