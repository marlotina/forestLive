using FL.Sendgrid.Implementation.Standard.Configuration.Contracts;
using Microsoft.Extensions.Configuration;

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

        public string SupportName => this.configuration.GetSection("SupportName").Get<string>();

        public string SupportEmail => this.configuration.GetSection("SupportEmail").Get<string>();

        public string ForgotPasswordTemplate => this.configuration.GetSection("ForgotPasswordTemplate").Get<string>();

        public string ConfirmAccountTemplate => this.configuration.GetSection("ConfirmAccountTemplate").Get<string>();

    }
}
