using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FL.CosmosDb.Standard.Configuration.Implementations
{
    public class CosmosConfiguration : ICosmosConfiguration
    {
        private readonly IConfiguration configuration;

        public CosmosConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfig DataConfig => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfig>();
    }
}
