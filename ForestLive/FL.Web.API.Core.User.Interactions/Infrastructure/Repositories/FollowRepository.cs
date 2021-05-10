﻿using FL.CosmosDb.Standard.Contracts;
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
        private IClientFactory clientFactory;
        private IVoteConfiguration voteConfiguration;
        private Container followContainer;

        public FollowRepository(IClientFactory clientFactory,
            IVoteConfiguration voteConfiguration)
        {
            this.clientFactory = clientFactory;
            this.voteConfiguration = voteConfiguration;
            this.followContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.voteConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
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
            }
            return false;

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
                return null;
            }
        }
    }
}
