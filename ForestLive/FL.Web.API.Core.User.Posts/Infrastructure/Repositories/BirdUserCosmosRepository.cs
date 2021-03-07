using FL.CosmosDb.Standard.Contracts;
using FL.WebAPI.Core.User.Posts.Configuration.Contracts;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using FL.WebAPI.Core.User.Posts.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Infrastructure.Repositories
{
    public class BirdUserCosmosRepository : IBirdUserRepository
    {
        private IClientFactory clientFactory;
        private IUserPostConfiguration userPostConfiguration;
        private Container usersContainer;

        public BirdUserCosmosRepository(IClientFactory clientFactory,
            IUserPostConfiguration userPostConfiguration)
        {
            this.clientFactory = clientFactory;
            this.userPostConfiguration = userPostConfiguration;
            this.usersContainer = InitialClient();
        }

        private Container InitialClient()
        {
            var config = this.userPostConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosUserContainer);
        }

        public async Task<List<BirdPost>> GetBlogPostsForUserId(string userId)
        {

            var blogPosts = new List<BirdPost>();


            var queryString = $"SELECT * FROM p WHERE p.type='post' AND p.userId = @UserId ORDER BY p.createDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            var query = this.usersContainer.GetItemQueryIterator<BirdPost>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                blogPosts.AddRange(response.ToList());
            }

            return blogPosts;
        }
    }
}
