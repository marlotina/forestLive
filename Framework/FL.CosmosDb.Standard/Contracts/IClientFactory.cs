using Microsoft.Azure.Cosmos;

namespace FL.CosmosDb.Standard.Contracts
{
    public interface IClientFactory
    {
        CosmosClient InitializeCosmosBlogClientInstanceAsync();
    }
}
