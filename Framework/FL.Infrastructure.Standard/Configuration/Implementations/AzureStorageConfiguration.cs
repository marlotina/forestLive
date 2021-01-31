using FL.Infrastructure.Standard.Configuration.Contracts;
using Microsoft.Extensions.Configuration;

namespace FL.Infrastructure.Standard.Configuration.Implementations
{
    public class AzureStorageConfiguration : IAzureStorageConfiguration
    {
        private readonly IConfiguration configuration;

        public AzureStorageConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AccountName => this.configuration.GetSection("AccountName").Value;

        public string AccountKey => this.configuration.GetSection("AccountKey").Value;
    }
    
}

