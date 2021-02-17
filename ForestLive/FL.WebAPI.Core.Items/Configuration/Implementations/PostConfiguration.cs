﻿using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.WebAPI.Core.Items.Configuration.Implementations
{
    public class PostConfiguration : IPostConfiguration
    {
        private readonly IConfiguration configuration;

        public PostConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfig CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfig>();

        public string Host => this.configuration.GetSection("Host").Get<string>();

        public ServiceBusConfig ServiceBusConfig => this.configuration.GetSection("ServiceBusConfig").Get<ServiceBusConfig>();

        public string BirdPhotoContainer => this.configuration.GetSection("BirdPhotoContainer").Get<string>();

        public string Secret => this.configuration.GetSection("Secret").Get<string>();
    }
}