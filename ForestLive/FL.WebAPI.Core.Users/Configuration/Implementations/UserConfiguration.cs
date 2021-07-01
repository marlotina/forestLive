using Microsoft.Extensions.Configuration;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Configuration.Dto;

namespace FL.WebAPI.Core.Users.Configuration.Implementations
{
    public class UserConfiguration : IUserConfiguration
    {
        private readonly IConfiguration configuration;

        public UserConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ImageProfileContainer => this.configuration.GetSection("ImageProfileContainer").Get<string>();

        public string Secret => this.configuration.GetSection("Secret").Get<string>();

        public string ConnectionStringUsersSite => this.configuration.GetSection("ConnectionStringUsersSite").Get<string>();

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();

        public string UserInteractionApiDomain => this.configuration.GetSection("UserInteractionApiDomain").Get<string>();

        public string FollowUrlService => this.configuration.GetSection("FollowUrlService").Get<string>();
    }
}