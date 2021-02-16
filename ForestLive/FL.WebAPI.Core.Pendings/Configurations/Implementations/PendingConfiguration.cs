using FL.WebAPI.Core.Pendings.Configurations.Contracts;
using Microsoft.Extensions.Configuration;

namespace FL.WebAPI.Core.Pendings.Configurations.Implementations
{
    public class PendingConfiguration : IPendingConfiguration
    {
        private readonly IConfiguration configuration;

        public PendingConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CosmosDatabaseId => this.configuration.GetSection("CosmosDatabaseId").Get<string>();

        public string CosmosPendingContainer => this.configuration.GetSection("CosmosPendingContainer").Get<string>();
    }
}
