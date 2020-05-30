using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace User.WebApi.Core.Integration.Tets.v1.Model.Requests
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
    }
}
