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
            await this.usersContainer.CreateItemAsync(post, new PartitionKey(post.UserId));
        }

        public async Task DeleteItemAsync(BirdPostDto deletePostRequest)
        {
            await this.usersContainer.DeleteItemAsync<BirdPostDto>(deletePostRequest.Id.ToString(), new PartitionKey(deletePostRequest.UserId));
        }
    }
}
