﻿using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Posts.Domain.Entities;
using FL.Web.API.Core.UserLabels.Domain.Dto;
using FL.WebAPI.Core.UserLabels.Configuration.Contracts;
using FL.WebAPI.Core.UserLabels.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.UserLabels.Infrastructure.Repositories
{
    public class UserLabelRepository : IUserLabelRepository
    {
        private readonly IClientFactory clientFactory;
        private readonly IUserLabelConfiguration iUserPostConfiguration;
        private readonly ILogger<UserLabelRepository> iLogger;
        private Container usersContainer;


        public UserLabelRepository(
            ILogger<UserLabelRepository> iLogger,
            IClientFactory clientFactory,
            IUserLabelConfiguration iUserPostConfiguration)
        {
            this.clientFactory = clientFactory;
            this.iUserPostConfiguration = iUserPostConfiguration;
            this.usersContainer = InitialClient();
            this.iLogger = iLogger;
        }

        private Container InitialClient()
        {
            var config = this.iUserPostConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosUserLabelContainer);
        }

        public async Task<List<UserLabelDto>> GetUserLabelsByUserId(string userId)
        {
            var labels = new List<UserLabelDto>();

            try
            {
                var queryString = $"SELECT p.id, p.postCount FROM p WHERE p.userId = @UserId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.usersContainer.GetItemQueryIterator<UserLabelDto>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    labels.AddRange(response.ToList());
                }
            }
            catch (Exception ex) 
            {
                this.iLogger.LogError(ex);
            }

            return labels;
        }

        public async Task<UserLabel> AddLabel(UserLabel userLabel)
        {
            try
            {
                return await this.usersContainer.CreateItemAsync<UserLabel>(userLabel, new PartitionKey(userLabel.UserId));
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return null;
            }
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
                this.iLogger.LogError(ex);
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
