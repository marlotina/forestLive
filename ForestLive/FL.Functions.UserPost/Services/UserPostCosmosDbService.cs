using FL.Functions.UserPost.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public class UserPostCosmosDbService : IUserPostCosmosDbService
    {
        private Container usersContainer;

        public UserPostCosmosDbService(CosmosClient dbClient, string databaseName)
        {
            this.usersContainer = dbClient.GetContainer(databaseName, "users");
        }

        public async Task CreatePostInPendingAsync(BirdPostDto post)
        {
            try
            {
                await usersContainer.CreateItemAsync(post, new PartitionKey(post.SpecieId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeletePostInPendingAsync(BirdPostDto post)
        {
            try
            {
                await this.usersContainer.DeleteItemAsync<BirdPostDto>(post.Id.ToString(), new PartitionKey(post.SpecieId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
