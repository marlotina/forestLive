using FL.WebAPI.Core.Birds.Configuration.Contracts;
using FL.WebAPI.Core.Birds.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.WebAPI.Core.Birds.Configuration.Implementations
{
    public class BirdsConfiguration : IBirdsConfiguration
    {
        private readonly IConfiguration configuration;

        public BirdsConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();

        public string VoteApiDomain => this.configuration.GetSection("VoteApiDomain").Get<string>();

        public string VoteUrlService => this.configuration.GetSection("VoteUrlService").Get<string>();
    }
}
