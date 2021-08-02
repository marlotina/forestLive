﻿using System;

namespace FL.WebAPI.Core.Account.Models.v1.Request
{
    public class ResetPasswordRequest
    {
        public Guid UserId { get; set; }
        
        public string NewPassword { get; set; }
        
        public string Code { get; set; }
    }
}
