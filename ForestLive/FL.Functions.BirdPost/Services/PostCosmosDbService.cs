using FL.Functions.BirdPost.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public class PostCosmosDbService : IPostCosmosDbService
    {
        private Container usersContainer;
        private Container pendingContainer;

        public PostCosmosDbService(CosmosClient dbClient, string databaseName)
        {
            usersContainer = dbClient.GetContainer(databaseName, "users");
            pendingContainer = dbClient.GetContainer(databaseName, "pending");
        }

        public async Task CreatePostInUserAsync(BirdPostDto post)
        {
            await usersContainer.CreateItemAsync(post, new PartitionKey(post.UserId));
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
    }
}
