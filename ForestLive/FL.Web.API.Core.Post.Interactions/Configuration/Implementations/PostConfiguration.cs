using FL.Web.API.Core.Post.Interactions.Configuration.Contracts;
using FL.Web.API.Core.Post.Interactions.Configuration.Dto;
using Microsoft.Extensions.Configuration;

namespace FL.Web.API.Core.Post.Interactions.Configuration.Implementations
{
    public class PostConfiguration : IPostConfiguration
    {
        private readonly IConfiguration configuration;

        public PostConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();

        public string Host => this.configuration.GetSection("Host").Get<string>();

        public ServiceBusConfig ServiceBusConfig => this.configuration.GetSection("ServiceBusConfig").Get<ServiceBusConfig>();

        public string Secret => this.configuration.GetSection("Secret").Get<string>();

        public string VoteApiDomain => this.configuration.GetSection("VoteApiDomain").Get<string>();

        public string VoteUrlService => this.configuration.GetSection("VoteUrlService").Get<string>();
    }
}
