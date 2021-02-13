using FL.Functions.BirdPost.Model;
using Microsoft.Azure.Cosmos;
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
            pendingContainer = dbClient.GetContainer(databaseName, "birds");
        }

        public async Task CreatePostInUserAsync(BirdPostDto post)
        {
            await usersContainer.CreateItemAsync(post, new Microsoft.Azure.Cosmos.PartitionKey(post.UserId));
        }

        public async Task CreatePostInPendingAsync(BirdPostDto post)
        {
            await pendingContainer.CreateItemAsync(post, new Microsoft.Azure.Cosmos.PartitionKey(post.CreateDate.ToString("ddMMyyyy")));
        }
    }
}
