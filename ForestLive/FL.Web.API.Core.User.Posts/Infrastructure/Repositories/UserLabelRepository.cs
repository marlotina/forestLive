using FL.CosmosDb.Standard.Contracts;
using FL.Web.API.Core.User.Posts.Domain.Repositories;
using FL.WebAPI.Core.User.Posts.Configuration.Contracts;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Infrastructure.Repositories
{
    public class UserLabelRepository : IUserLabelRepository
    {
        private IClientFactory clientFactory;
        private IUserPostConfiguration userPostConfiguration;
        private Container usersContainer;

        public UserLabelRepository(IClientFactory clientFactory,
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
        public async Task<List<string>> GetUserLabels(string userId)
        {
            var labels = new List<string>();


            var queryString = $"SELECT p.label FROM p WHERE p.type='label' AND p.userId = @UserId";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            var query = this.usersContainer.GetItemQueryIterator<string>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                labels.AddRange(response.ToList());
            }

            return labels;
        }
    }
}
