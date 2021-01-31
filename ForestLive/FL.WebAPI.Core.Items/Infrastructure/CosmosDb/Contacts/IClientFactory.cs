using Microsoft.Azure.Cosmos;

namespace FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Contracts
{
    public interface IClientFactory
    {
        CosmosClient InitializeCosmosBlogClientInstanceAsync();
    }
}
