using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FL.Sendgrid.Implementation.Standard.Configuration.Models
{
    public class AccountTemplateModel
    {

        [JsonProperty("UserName")]
        public string UserName { get; set; }


        [JsonProperty("Token")]
        public string Token { get; set; }


        [JsonProperty("UserId")]
        public string UserId { get; set; }
    }
}
