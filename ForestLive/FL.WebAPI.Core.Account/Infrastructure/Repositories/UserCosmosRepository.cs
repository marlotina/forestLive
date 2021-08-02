using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Account.Configuration.Contracts;
using FL.WebAPI.Core.Account.Domain.Entities;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class UserCosmosRepository : IUserCosmosRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IUserConfiguration iUserConfiguration; 
        private Container userContainer;
        private readonly ILogger<UserCosmosRepository> iLogger;
        public UserCosmosRepository(
            IClientFactory iClientFactory,
            ILogger<UserCosmosRepository> iLogger,
            IUserConfiguration iPostConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iUserConfiguration = iPostConfiguration;
            this.userContainer = InitialCLient();
            this.iLogger = iLogger;
        }

        private Container InitialCLient()
        {
            var config = this.iUserConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosUserContainer);
        }

        public async Task<UserInfo> CreateUserInfoAsync(UserInfo user)
        {
            try
            {
                return await this.userContainer.CreateItemAsync<UserInfo>(user, new PartitionKey(user.Id));
            }
            catch (Exception ex) 
            {
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<string> GetUserImage(string userId, string userNameId)
        {
            try
            {
                var response = await this.userContainer.ReadItemAsync<UserInfo>(userId, new PartitionKey(userNameId));
                var ru = response.RequestCharge;
                return response.Resource.Photo;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }
    }
}
