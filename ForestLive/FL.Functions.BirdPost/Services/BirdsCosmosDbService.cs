using FL.Functions.BirdPost.Model;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public class BirdsCosmosDbService : IBirdsCosmosDbService
    {
        private Container birdsContainer;

        public BirdsCosmosDbService(CosmosClient dbClient, string databaseName)
        {
            this.birdsContainer = dbClient.GetContainer(databaseName, "birds");
        }

        public async Task CreatePostInUserAsync(BirdPostDto post)
        {
            await this.birdsContainer.CreateItemAsync(post, new PartitionKey(post.UserId));
        }

        public async Task DeleteItemAsync(BirdPostDto deletePostRequest)
        {
            await this.birdsContainer.DeleteItemAsync<BirdPostDto>(deletePostRequest.Id.ToString(), new PartitionKey(deletePostRequest.UserId));
        }
    }
}
