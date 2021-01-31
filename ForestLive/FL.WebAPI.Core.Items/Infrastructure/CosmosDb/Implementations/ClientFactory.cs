using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Contracts;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Implementations
{

    public class ClientFactory : IClientFactory
    {
        private readonly IItemConfiguration itemConfiguration;

        public ClientFactory(IItemConfiguration itemConfiguration)
        {
            this.itemConfiguration = itemConfiguration;
        }

        public CosmosClient InitializeCosmosBlogClientInstanceAsync()
        {
            var config = this.itemConfiguration.CosmosConfiguration;

            string databaseName = config.CosmosDatabaseId;
            string account = config.CosmosdbConnection;
            string key = config.CosmosKey;

            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(account, key);
            CosmosClient client = clientBuilder
                .WithApplicationName(databaseName)
                .WithApplicationName(Regions.WestEurope)
                .WithConnectionModeDirect()
                .WithSerializerOptions(new CosmosSerializationOptions() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
                .Build();

            return client;
        }
    }
}
