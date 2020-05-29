using FL.Sendgrid.Implementation.Standard.Configuration.Contracts;
using FL.Sendgrid.Implementation.Standard.Configuration.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace FL.Sendgrid.Implementation.Standard.Configuration.Implementations
{
    public class MailConfiguration : IMailConfiguration
    {
        private readonly IConfiguration configuration;

        public MailConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string SendgridApiKey => this.configuration.GetSection("SendgridApiKey").Get<string>();

        public List<EmailItemConfiguration> EmailTemaplateList => this.configuration.GetSection("EmailTemaplateList").Get<List<EmailItemConfiguration>>();
    }
}
