using Microsoft.Extensions.Configuration;
using FL.WebAPI.Core.Account.Configuration.Contracts;
using FL.WebAPI.Core.Account.Configuration.Dto;

namespace FL.WebAPI.Core.Account.Configuration.Implementations
{
    public class UserConfiguration : IUserConfiguration
    {
        private readonly IConfiguration configuration;

        public UserConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Secret => this.configuration.GetSection("Secret").Get<string>();

        public string ConnectionStringUsersSite => this.configuration.GetSection("ConnectionStringUsersSite").Get<string>();

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();
    }
}