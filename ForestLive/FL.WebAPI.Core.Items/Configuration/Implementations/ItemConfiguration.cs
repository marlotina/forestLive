using FL.WebAPI.Core.Items.Configuration.Contracts;
using Microsoft.Extensions.Configuration;
using System;

namespace FL.WebAPI.Core.Items.Configuration.Implementations
{
    public class ItemConfiguration : IItemConfiguration
    {
        private readonly IConfiguration configuration;

        public ItemConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string BirdPhotoContainer => this.configuration.GetSection("BirthPhotoContainer").Get<string>();

        public string Secret => this.configuration.GetSection("Secret").Get<string>();

        public string Host => this.configuration.GetSection("Host").Get<string>();

        public string PrimaryKey => this.configuration.GetSection("PrimaryKey").Get<string>();

        public string Database => this.configuration.GetSection("DatabaseName").Get<string>();

        public string Container => this.configuration.GetSection("ContainerName").Get<string>();

        public string CosmosdbConnectionstring => this.configuration.GetSection("CosmosdbConnectionstring").Get<string>();

        public string CosmosDatabaseId => this.configuration.GetSection("CosmosDatabaseId").Get<string>();

        public string CosmosContainerId => this.configuration.GetSection("CosmosContainerId").Get<string>();
    }
}
