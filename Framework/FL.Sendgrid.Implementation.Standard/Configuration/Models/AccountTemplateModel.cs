using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FL.Sendgrid.Implementation.Standard.Configuration.Models
{
    public class AccountTemplateModel
    {

        [JsonProperty("userName")]
        public string UserName { get; set; }


        [JsonProperty("token")]
        public string Token { get; set; }


        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
