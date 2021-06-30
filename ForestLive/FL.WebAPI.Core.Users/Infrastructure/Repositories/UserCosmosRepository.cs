using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Domain.Entities;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class UserCosmosRepository : IUserCosmosRepository
    {
        private IClientFactory iClientFactory;
        private IUserConfiguration iUserConfiguration; 
        private Container userContainer;
        private ILogger<UserCosmosRepository> iLogger;
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

        public async Task<bool> UpdateUserInfoAsync(UserInfo user)
        {
            try
            {
                await this.userContainer.UpsertItemAsync<UserInfo>(user, new PartitionKey(user.Id));
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUserInfoAsync(string userId, string userNameId)
        {

            try
            {
                await this.userContainer.DeleteItemAsync<UserInfo>(userId, new PartitionKey(userNameId));
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<UserInfo> GetUser(string userId, string userNameId)
        {
            try
            {
                var response = await this.userContainer.ReadItemAsync<UserInfo>(userId, new PartitionKey(userNameId));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<UserInfo>> GetUsersAsync()
        {
            var users = new List<UserInfo>();

            try
            {
                var queryString = $"SELECT * FROM p WHERE p.type='user'";

                var queryDef = new QueryDefinition(queryString);
                var query = this.userContainer.GetItemQueryIterator<UserInfo>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    users.AddRange(response.ToList());
                }
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
            }

            return users;
        }

        public async Task<UserInfo> GetUserByName(string userNameId)
        {
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.userId = @UserId AND p.type='user' ";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userNameId);

                var query = this.userContainer.GetItemQueryIterator<UserInfo>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    return response.Resource.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }

            return null;
        }
    }
}
