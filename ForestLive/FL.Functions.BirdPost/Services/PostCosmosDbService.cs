using FL.Functions.BirdPost.Model;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public class PostCosmosDbService : IPostCosmosDbService
    {
        private Container usersContainer;

        public PostCosmosDbService(CosmosClient dbClient, string databaseName)
        {
            usersContainer = dbClient.GetContainer(databaseName, "users");
        }

        public async Task CreatePostInUserAsync(BirdPostDto post)
        {
            await usersContainer.CreateItemAsync(post, new PartitionKey(post.UserId));
        }
    }
}
