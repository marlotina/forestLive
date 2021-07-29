using FL.BlobContainer.Standard.Configuration.Contracts;
using FL.BlobContainer.Standard.Configuration.Dto;
using Microsoft.Extensions.Configuration;

namespace FL.BlobContainer.Standard.Configuration.Implementations
{
    public class AzureStorageConfiguration : IAzureStorageConfiguration
    {
        private readonly IConfiguration configuration;

        public AzureStorageConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AccountName => this.configuration.GetSection("StorageAccountName").Value;

        public string AccountKey => this.configuration.GetSection("StorageAccountKey").Value;

        public StorageConfig StorageConfiguration => this.configuration.GetSection("StorageConfiguration").Get<StorageConfig>();
    }
    
}

