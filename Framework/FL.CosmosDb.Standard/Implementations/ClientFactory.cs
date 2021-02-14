﻿using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Contracts;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace FL.CosmosDb.Standard.Implementations
{
    public class ClientFactory : IClientFactory
    {
        private readonly ICosmosConfiguration cosmosConfiguration;
        private CosmosClient client;

        public ClientFactory(ICosmosConfiguration cosmosConfiguration)
        {
            this.cosmosConfiguration = cosmosConfiguration;
        }

        public CosmosClient InitializeCosmosBlogClientInstanceAsync()
        {
            if (this.client == null)
            {
                var config = this.cosmosConfiguration.DataConfig;

                var databaseName = config.CosmosDatabaseId;
                var account = config.CosmosdbConnection;
                var key = config.CosmosKey;

                CosmosClientBuilder clientBuilder = new CosmosClientBuilder(account, key);
                this.client = clientBuilder
                    .WithApplicationName(databaseName)
                    .WithConnectionModeDirect()
                    .WithSerializerOptions(new CosmosSerializationOptions() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
                    .Build();
            }

            return this.client;
        }
    }
}

