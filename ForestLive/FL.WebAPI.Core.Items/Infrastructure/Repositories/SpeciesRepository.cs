﻿using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repository;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IPostConfiguration iPostConfiguration;
        private readonly Container birdContainer;
        private readonly ILogger<SpeciesRepository> iLogger;

        public SpeciesRepository(
            ILogger<SpeciesRepository> iLogger,
            IClientFactory iClientFactory,
            IPostConfiguration iPostConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iPostConfiguration = iPostConfiguration;
            this.iLogger = iLogger;
            this.birdContainer = this.InitialClient();
        }

        private Container InitialClient()
        {
            var config = this.iPostConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosSpecieContainer);
        }

        public async Task<bool> CreatePostAsync(BirdPost post)
        {
            try
            {
                await this.birdContainer.CreateItemAsync(post, new PartitionKey(post.SpecieId.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<BirdPost> GetPostsAsync(Guid postId, Guid specieId)
        {
            try
            {
                var response = await this.birdContainer.ReadItemAsync<BirdPost>(postId.ToString(), new PartitionKey(specieId.ToString()));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeletePostAsync(Guid postId, Guid specieId)
        {
            try
            {
                await this.birdContainer.DeleteItemAsync<BirdPost>(postId.ToString(), new PartitionKey(specieId.ToString()));
                return true;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }
    }
}
