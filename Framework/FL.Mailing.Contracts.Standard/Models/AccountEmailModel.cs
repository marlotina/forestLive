using System;
using System.Collections.Generic;
using System.Text;

namespace FL.Mailing.Contracts.Standard.Models
{
    public class AccountEmailModel
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Code { get; set; }
    }
}
