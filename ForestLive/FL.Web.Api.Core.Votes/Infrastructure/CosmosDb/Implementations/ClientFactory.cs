using FL.Web.Api.Core.Votes.Configuration.Contracts;
using FL.Web.Api.Core.Votes.Infrastructure.CosmosDb.Contracts;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace FL.Web.Api.Core.Votes.Infrastructure.CosmosDb.Implementations
{

    public class ClientFactory : IClientFactory
    {
        private readonly IPostConfiguration itemConfiguration;
        private CosmosClient client;

        public ClientFactory(IPostConfiguration itemConfiguration)
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
