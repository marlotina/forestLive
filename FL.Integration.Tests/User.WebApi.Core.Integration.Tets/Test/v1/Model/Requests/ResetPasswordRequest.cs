using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace User.WebApi.Core.Integration.Tets.v1.Model.Requests
{
    public class ResetPasswordRequest
    {
        public Guid UserId { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
