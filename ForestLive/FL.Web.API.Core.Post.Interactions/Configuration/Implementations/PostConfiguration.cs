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

        public ServiceBusConfig ServiceBusConfig => this.configuration.GetSection("ServiceBusConfig").Get<ServiceBusConfig>();

        public string UserUrlService => this.configuration.GetSection("UserUrlService").Get<string>();

        public string UserApiDomain => this.configuration.GetSection("UserApiDomain").Get<string>();
    }
}
