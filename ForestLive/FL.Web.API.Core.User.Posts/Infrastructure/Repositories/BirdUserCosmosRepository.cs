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

        public async Task<List<BirdPost>> GetPostsByUserId(string userId)
        {
            var posts = new List<BirdPost>();


            var queryString = $"SELECT * FROM p WHERE p.type='post' AND p.userId = @UserId ORDER BY p.creationDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            var query = this.usersContainer.GetItemQueryIterator<BirdPost>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }

        public async Task<List<BirdPost>> GetMapPointsForUserId(string userId)
        {
            var posts = new List<BirdPost>();


            var queryString = $"SELECT p.postId, p.location FROM p WHERE p.type='post' AND p.userId = @UserId ORDER BY p.creationDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            var query = this.usersContainer.GetItemQueryIterator<BirdPost>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }

        public async Task<BirdPost> GetPostsByPostId(string postId, string userId)
        {
            try
            {
                var response = await this.usersContainer.ReadItemAsync<BirdPost>(postId.ToString(), new PartitionKey(userId));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<BirdPost>> GetPostsByLabelByUserId(string label, string userId)
        {
            var posts = new List<BirdPost>();

            var queryString = $"SELECT * FROM p WHERE p.type='post' AND p.userId = @UserId AND ARRAY_CONTAINS(p.labels, @Label)";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            queryDef.WithParameter("@Label", label);
            var query = this.usersContainer.GetItemQueryIterator<BirdPost>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }
    }
}
