using FL.CosmosDb.Standard.Contracts;
using FL.Web.API.Core.User.Posts.Domain.Entities;
using FL.Web.API.Core.User.Posts.Domain.Repositories;
using FL.WebAPI.Core.User.Posts.Configuration.Contracts;
using Microsoft.Azure.Cosmos;
using System;
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
        public async Task<List<LabelDto>> GetUserLabelsByUserId(string userId)
        {
            var labels = new List<LabelDto>();

            try
            {
                var queryString = $"SELECT p.id FROM p WHERE p.type='label' AND p.userId = @UserId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.usersContainer.GetItemQueryIterator<LabelDto>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    labels.AddRange(response.ToList());
                }
            }
            catch (Exception ex) 
            { 
            }

            return labels;
        }

        public async Task<UserLabel> AddLabel(UserLabel userLabel)
        {
            return await this.usersContainer.CreateItemAsync<UserLabel>(userLabel, new PartitionKey(userLabel.UserId));
        }

        public async Task<bool> DeleteLabel(UserLabel userLabel)
        {
            try
            {
                await this.usersContainer.DeleteItemAsync<UserLabel>(userLabel.Id, new PartitionKey(userLabel.UserId));
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
            
        }

        public async Task<UserLabel> GetUserLabel(string label, string userId)
        {
            try
            {
                var response = await this.usersContainer.ReadItemAsync<UserLabel>(label, new PartitionKey(userId));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public async Task<List<UserLabel>> GetUserLabelsDetails(string userId)
        {
            var labels = new List<UserLabel>();

            try
            {
                var queryString = $"SELECT * FROM p WHERE p.type='label' AND p.userId = @UserId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.usersContainer.GetItemQueryIterator<UserLabel>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    labels.AddRange(response.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            return labels;
        }
    }
}
