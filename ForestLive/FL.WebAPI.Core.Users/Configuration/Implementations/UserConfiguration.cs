using Microsoft.Extensions.Configuration;
using FL.WebAPI.Core.Users.Configuration.Contracts;

namespace FL.WebAPI.Core.Users.Configuration.Implementations
{
    public class UserConfiguration : IUserConfiguration
    {
        private readonly IConfiguration configuration;

        public UserConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AccountName => this.configuration.GetSection("AccountName").Get<string>();

        public string AccountKey => this.configuration.GetSection("AccountKey").Get<string>();

        public string ImageContainer => "images";

        public string SendgridApiKey => this.configuration.GetSection("SendgridApiKey").Get<string>();

        public string SupportName => this.configuration.GetSection("SupportName").Get<string>();

        public string SupportEmail => this.configuration.GetSection("SupportEmail").Get<string>();

        public string Secret => this.configuration.GetSection("Secret").Get<string>();
        
        public string UrlConfirmEmail => this.configuration.GetSection("UrlConfirmEmail").Get<string>();
        
        public string ConnectionStringUsersSite => this.configuration.GetSection("ConnectionStringUsersSite").Get<string>();

        public string UrlForgotPasswordEmail => this.configuration.GetSection("UrlForgotPasswordEmail").Get<string>();
    }
}
