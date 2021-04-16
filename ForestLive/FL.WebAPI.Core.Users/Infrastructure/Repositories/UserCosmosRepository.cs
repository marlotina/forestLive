using FL.CosmosDb.Standard.Contracts;
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

        public async Task<bool> UpdateUserInfoAsync(UserInfo user)
        {
             await this.userContainer.UpsertItemAsync<UserInfo>(user, new PartitionKey(user.UserId.ToString()));
            return true;
        }

        public async Task DeleteUserInfoAsync(Guid userId, string userNameId)
        {
            await this.userContainer.DeleteItemAsync<UserInfo>(userId.ToString(), new PartitionKey(userNameId));
        }

        public async Task<UserInfo> GetUser(Guid userId, string userNameId)
        {
            try
            {
                var response = await this.userContainer.ReadItemAsync<UserInfo>(userId.ToString(), new PartitionKey(userNameId));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserInfo>> GetUsersAsync()
        {
            var posts = new List<UserInfo>();

            var queryString = $"SELECT * FROM p";

            var queryDef = new QueryDefinition(queryString);
            var query = this.userContainer.GetItemQueryIterator<UserInfo>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }

        public async Task<UserInfo> GetUserByName(string userNameId)
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

            return null;
        }
    }
}
