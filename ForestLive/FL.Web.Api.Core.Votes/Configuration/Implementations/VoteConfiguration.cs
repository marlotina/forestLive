﻿using FL.Web.Api.Core.Votes.Configuration.Contracts;
using FL.Web.Api.Core.Votes.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.Web.Api.Core.Votes.Configuration.Implementations
{
    public class VoteConfiguration : IVoteConfiguration
    {
        private readonly IConfiguration configuration;

        public VoteConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfig CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfig>();

        public string Host => this.configuration.GetSection("Host").Get<string>();

        public ServiceBusConfig ServiceBusConfig => this.configuration.GetSection("ServiceBusConfig").Get<ServiceBusConfig>();

        public string Secret => this.configuration.GetSection("Secret").Get<string>();
    }
}
