using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities.User;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Contracts;
using Microsoft.Azure.Cosmos;
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
            await usersContainer.CreateItemAsync<UserBird>(user, new PartitionKey(user.UserId.ToString()));
            //await _usersContainer.CreateItemAsync<BlogUser>(user, new PartitionKey(user.UserId), new ItemRequestOptions { PreTriggers = new List<string> { "validateUserUsernameNotExists" } });
        }
    }
}
