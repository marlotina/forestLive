using FL.Web.API.Core.Bird.Pending.Configuration.Contracts;
using FL.Web.API.Core.Bird.Pending.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.Web.API.Core.Bird.Pending.Configuration.Implementations
{
    public class BirdPendingConfiguration : IBirdPendingConfiguration
    {
        private readonly IConfiguration configuration;

        public BirdPendingConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();

        public string VoteApiDomain => this.configuration.GetSection("VoteApiDomain").Get<string>();

        public string VoteUrlService => this.configuration.GetSection("VoteUrlService").Get<string>();
    }
}
