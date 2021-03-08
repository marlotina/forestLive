using FL.WebAPI.Core.User.Posts.Configuration.Contracts;
using FL.WebAPI.Core.User.Posts.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.WebAPI.Core.User.Posts.Configuration.Implementations
{
    public class UserPostConfiguration : IUserPostConfiguration
    {
        private readonly IConfiguration configuration;

        public UserPostConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();

        public string VoteApiDomain => this.configuration.GetSection("VoteApiDomain").Get<string>();

        public string VoteUrlService => this.configuration.GetSection("VoteUrlService").Get<string>();
    }
}
