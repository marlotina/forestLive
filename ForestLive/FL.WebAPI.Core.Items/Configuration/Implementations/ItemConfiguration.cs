﻿using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Configuration.Models;
using Microsoft.Extensions.Configuration;

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

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();

        public string Host => this.configuration.GetSection("Host").Get<string>();
    }
}
