using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Configuration.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Infrastructure.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private IClientFactory iClientFactory;
        private IVoteConfiguration iVoteConfiguration;
        private readonly ILogger<FollowRepository> iLogger;
        private Container followContainer;

        public FollowRepository(
            IClientFactory iClientFactory,
            IVoteConfiguration iVoteConfiguration,
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
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosFollowContainer);
        }

        public async Task<FollowUser> AddFollow(FollowUser followUser)
        {
            try
            {
                return await this.followContainer.CreateItemAsync<FollowUser>(followUser, new PartitionKey(followUser.UserId));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteFollow(string userId, string id)
        {
            try
            {
                await this.followContainer.DeleteItemAsync<FollowUser>(userId, new PartitionKey(id));
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
    }
}
