using FL.Functions.BirdPost.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            await usersContainer.CreateItemAsync(post, new Microsoft.Azure.Cosmos.PartitionKey(post.UserId));
        }

        public async Task CreatePostInPendingAsync(BirdPostDto post)
        {
            if (string.IsNullOrEmpty(post.SpecieName) || (post.SpecieId == null || post.SpecieId == Guid.Empty)) {
                post.Type = "withSpecie";
            }
            else {
                post.Type = "withoutSpecie";
            }

            await pendingContainer.CreateItemAsync(post, new Microsoft.Azure.Cosmos.PartitionKey(post.UserId));
        }
    }
}
