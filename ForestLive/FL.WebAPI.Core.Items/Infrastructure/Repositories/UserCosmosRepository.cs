using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Entities.User;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Contracts;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class UserCosmosRepository : IUserRepository
    {
        private IClientFactory clientFactory;
        private IItemConfiguration itemConfiguration;
        private Container usersContainer;

        public UserCosmosRepository(IClientFactory clientFactory,
            IItemConfiguration itemConfiguration)
        {
            this.clientFactory = clientFactory;
            this.itemConfiguration = itemConfiguration;
            this.usersContainer = InitialClient();// dbClient.GetContainer(databaseName, "birdItems");
        }

        private Container InitialClient()
        {
            var config = this.itemConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync();
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosUserContainer);
        }

        public async Task CreateUserAsync(UserBird user)
        {
            await usersContainer.CreateItemAsync<UserBird>(user, new PartitionKey(user.UserId));
        }

        public async Task<List<Item>> GetBlogPostsForUserId(string userId)
        {

            var blogPosts = new List<Item>();


            var queryString = $"SELECT * FROM p WHERE p.type='post' AND p.userId = @UserId ORDER BY p.createDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            var query = this.usersContainer.GetItemQueryIterator<Item>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                blogPosts.AddRange(response.ToList());
            }

            return blogPosts;
        }

        public async Task CreateItemAsync(Item item)
        {
            await this.usersContainer.CreateItemAsync<Item>(item, new PartitionKey(item.UserId));
        }
    }
}
