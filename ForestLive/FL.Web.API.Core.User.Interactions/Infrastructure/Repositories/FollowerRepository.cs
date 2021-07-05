using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Configuration.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Infrastructure.Repositories
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IUserInteractionsConfiguration iVoteConfiguration;
        private readonly ILogger<FollowRepository> iLogger;
        private readonly Container followerContainer;

        public FollowerRepository(
            IClientFactory iClientFactory,
            IUserInteractionsConfiguration iVoteConfiguration,
            ILogger<FollowRepository> iLogger)
        {
            this.iClientFactory = iClientFactory;
            this.iVoteConfiguration = iVoteConfiguration;
            this.followerContainer = InitialCLient();
            this.iLogger = iLogger;
        }

        private Container InitialCLient()
        {
            var config = this.iVoteConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosUserContainer);
        }

        public async Task<List<FollowUser>> GetFollowersByUserId(string userId)
        {
            var follow = new List<FollowUser>();
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.type='follower' AND p.userId = @UserId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.followerContainer.GetItemQueryIterator<FollowUser>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    follow.AddRange(response.ToList());
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }

            return follow;
        }
    }
}
