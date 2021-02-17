﻿using FL.WebAPI.Core.Items.Configuration.Models;

namespace FL.WebAPI.Core.Items.Configuration.Contracts
{
    public interface IPostConfiguration
    {
        string BirdPhotoContainer { get; }

        string Secret { get; }

        string Host { get; }

        CosmosConfig CosmosConfiguration { get; }

        ServiceBusConfig ServiceBusConfig { get;  }
    }
}