using FL.Web.API.Core.User.Interactions.Configuration.Contracts;
using FL.Web.API.Core.User.Interactions.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.Web.API.Core.User.Interactions.Configuration.Implementations
{
    public class VoteConfiguration : IVoteConfiguration
    {
        private readonly IConfiguration configuration;

        public VoteConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();

        public string Host => this.configuration.GetSection("Host").Get<string>();

        public string Secret => this.configuration.GetSection("Secret").Get<string>();
    }
}
