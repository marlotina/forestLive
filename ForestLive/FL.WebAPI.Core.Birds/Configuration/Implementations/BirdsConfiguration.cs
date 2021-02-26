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

        public string ConnectionString => this.configuration.GetSection("ConnectionString").Get<string>();

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();
    }
}
