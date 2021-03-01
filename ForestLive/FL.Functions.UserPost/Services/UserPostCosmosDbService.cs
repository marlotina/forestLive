using FL.Functions.UserPost.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public class UserPostCosmosDbService : IUserPostCosmosDbService
    {
        private Container pendingContainer;

        public UserPostCosmosDbService(CosmosClient dbClient, string databaseName)
        {
            pendingContainer = dbClient.GetContainer(databaseName, "birds");
        }

        public async Task CreatePostInPendingAsync(BirdPostDto post)
        {
            try
            {
                await pendingContainer.CreateItemAsync(post, new PartitionKey(post.SpecieId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeletePostInPendingAsync(BirdPostDto post)
        {
            try
            {
                await this.pendingContainer.DeleteItemAsync<BirdPostDto>(post.Id.ToString(), new PartitionKey(post.SpecieId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
