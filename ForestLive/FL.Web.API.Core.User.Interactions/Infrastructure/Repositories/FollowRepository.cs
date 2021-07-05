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
    public class FollowRepository : IFollowRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IUserInteractionsConfiguration iVoteConfiguration;
        private readonly ILogger<FollowRepository> iLogger;
        private readonly Container followContainer;

        public FollowRepository(
            IClientFactory iClientFactory,
            IUserInteractionsConfiguration iVoteConfiguration,
            ILogger<FollowRepository> iLogger)
        {
            this.iClientFactory = iClientFactory;
            this.iVoteConfiguration = iVoteConfiguration;
            this.followContainer = InitialCLient();
            this.iLogger = iLogger;
        }

        private Container InitialCLient()
        {
            var config = this.iVoteConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosUserContainer);
        }

        public async Task<Follow> AddFollow(Follow followUser)
        {
            try
            {
                return await this.followContainer.CreateItemAsync<Follow>(followUser, new PartitionKey(followUser.UserId));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteFollow(string followId, string userId)
        {
            try
            {
                await this.followContainer.DeleteItemAsync<FollowUser>(followId, new PartitionKey(userId));
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<FollowUser> GetFollow(string userId, string followUserId)
        {

            try
            {
                var response = await this.followContainer.ReadItemAsync<FollowUser>(followUserId, new PartitionKey(userId));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<FollowUser>> GetFollowByUserId(string userId)
        {
            var follow = new List<FollowUser>();
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.type='follow' AND p.userId = @UserId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.followContainer.GetItemQueryIterator<FollowUser>(queryDef);

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
