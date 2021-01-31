using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Contracts;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Implementations
{

    public class ClientFactory : IClientFactory
    {
        private readonly IItemConfiguration itemConfiguration;
        private CosmosClient client;

        public ClientFactory(IItemConfiguration itemConfiguration)
        {
            this.itemConfiguration = itemConfiguration;
        }

        public CosmosClient InitializeCosmosBlogClientInstanceAsync()
        {
            if (this.client == null) {
                var config = this.itemConfiguration.CosmosConfiguration;

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
