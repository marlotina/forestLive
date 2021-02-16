using FL.Functions.Pending.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.Pending.Services
{
    public class PendingCosmosDbService : IPendingCosmosDbService
    {
        private Container pendingContainer;

        public PendingCosmosDbService(CosmosClient dbClient, string databaseName)
        {
            pendingContainer = dbClient.GetContainer(databaseName, "pending");
        }

        public async Task CreatePostInPendingAsync(BirdPostDto post)
        {
            try
            {
                await pendingContainer.CreateItemAsync(post, new PartitionKey(post.SpecieStatus));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeletePostInPendingAsync(BirdPostDto post)
        {
            try
            {
                await this.pendingContainer.DeleteItemAsync<BirdPostDto>(post.Id.ToString(), new PartitionKey(post.SpecieStatus));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
