using FL.CosmosDb.Standard.Contracts;
using FL.WebAPI.Core.Pendings.Configurations.Contracts;
using FL.WebAPI.Core.Pendings.Domain.Entities;
using FL.WebAPI.Core.Pendings.Domain.Repository;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Pendings.Infrastructure.Repository
{
    public class PendingPostRepository : IPendingPostRepository
    {
        private IClientFactory clientFactory;
        private IPendingConfiguration pendingConfiguration;
        private Container usersContainer;

        public PendingPostRepository(
            IClientFactory clientFactory,
            IPendingConfiguration pendingConfiguration)
        {
            this.clientFactory = clientFactory;
            this.pendingConfiguration = pendingConfiguration;
            this.usersContainer = InitialClient();
        }

        private Container InitialClient()
        {
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync();
            return dbClient.GetContainer(this.pendingConfiguration.CosmosDatabaseId, this.pendingConfiguration.CosmosPendingContainer);
        }

        public async Task<List<BirdPost>> GetPendingPostsByStatus(string status)
        {

            var blogPosts = new List<BirdPost>();


            var queryString = $"SELECT * FROM p WHERE p.specieStatus = @specieStatus ORDER BY p.createDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@specieStatus", status);
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
