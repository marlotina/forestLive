using FL.CosmosDb.Standard.Contracts;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Domain.Entities;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class UserCosmosRepository : IUserCosmosRepository
    {
        private IClientFactory iClientFactory;
        private IUserConfiguration iUserConfiguration; 
        private Container userContainer;

        public UserCosmosRepository(IClientFactory iClientFactory,
            IUserConfiguration iPostConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iUserConfiguration = iPostConfiguration;
            this.userContainer = InitialCLient();
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
                return await this.userContainer.CreateItemAsync<UserInfo>(user, new PartitionKey(user.UserId.ToString()));
            }
            catch (Exception ex) 
            {
                return null;
            }
            
        }

        public async Task<UserInfo> UpdateUserInfoAsync(UserInfo user)
        {
            try
            {
                return await this.userContainer.UpsertItemAsync<UserInfo>(user, new PartitionKey(user.UserId.ToString()));
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task DeleteUserInfoAsync(string userId)
        {
            await this.userContainer.DeleteItemAsync<UserInfo>(userId, new PartitionKey(userId));
        }

        public async Task<UserInfo> GetUser(string userId)
        {
            try
            {
                var response = await this.userContainer.ReadItemAsync<UserInfo>(userId, new PartitionKey(userId));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
