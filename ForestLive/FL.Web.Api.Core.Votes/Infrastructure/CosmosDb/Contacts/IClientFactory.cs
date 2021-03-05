using Microsoft.Azure.Cosmos;

namespace FL.Web.Api.Core.Votes.Infrastructure.CosmosDb.Contracts
{
    public interface IClientFactory
    {
        CosmosClient InitializeCosmosBlogClientInstanceAsync();
    }
}
