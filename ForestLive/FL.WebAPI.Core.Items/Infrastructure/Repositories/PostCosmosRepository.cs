using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Dto;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class PostCosmosRepository : IPostRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IPostConfiguration iPostConfiguration;
        private readonly ILogger<PostCosmosRepository> iLogger;
        private Container postContainer;

        public PostCosmosRepository(
            ILogger<PostCosmosRepository> iLogger,
            IClientFactory iClientFactory,
            IPostConfiguration iPostConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iPostConfiguration = iPostConfiguration;
            this.postContainer = InitialCLient();
            this.iLogger = iLogger;
        }

        private Container InitialCLient() {
            var config = this.iPostConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosPostContainer);
        }

        public async Task<bool> CreatePostAsync(BirdPost post)
        {
            try
            {
                await this.postContainer.CreateItemAsync<BirdPost>(post, new PartitionKey(post.PostId.ToString()));
                return true;
            }
            catch (Exception ex) 
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }

        }

        public async Task<bool> DeletePostAsync(Guid id, string partitionKey)
        {
            try
            {
                await this.postContainer.DeleteItemAsync<BirdPost>(id.ToString(), new PartitionKey(partitionKey));
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdatePostAsync(BirdPost post)
        {
            try
            {
                await this.postContainer.UpsertItemAsync<BirdPost>(post, new PartitionKey(post.PostId.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<BirdPost> GetPostAsync(Guid postId)
        {
            try
            {
                var response = await this.postContainer.ReadItemAsync<BirdPost>(postId.ToString(), new PartitionKey(postId.ToString()));
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
