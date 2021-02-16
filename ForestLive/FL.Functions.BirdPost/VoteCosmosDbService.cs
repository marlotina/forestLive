using Microsoft.Azure.Cosmos;

namespace FL.Functions.BirdPost
{
    internal class VoteCosmosDbService
    {
        private CosmosClient client;
        private string databaseName;

        public VoteCosmosDbService(CosmosClient client, string databaseName)
        {
            this.client = client;
            this.databaseName = databaseName;
        }
    }
}