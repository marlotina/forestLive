﻿using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Contracts;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class BirdPostCosmosRepository : IBIrdPostRepository
    {
        private IClientFactory clientFactory;
        private IItemConfiguration itemConfiguration; 
        private Container postContainer;

        public BirdPostCosmosRepository(IClientFactory clientFactory,
            IItemConfiguration itemConfiguration)
        {
            this.clientFactory = clientFactory;
            this.itemConfiguration = itemConfiguration;
            this.postContainer = InitialCLient();
        }

        private Container InitialCLient() {
            var config = this.itemConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync();
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosBirdContainer);
        }

        public async Task CreatePostAsync(BirdPost item)
        {
            await this.postContainer.CreateItemAsync<BirdPost>(item, new PartitionKey(item.PostId.ToString()));
        }

        public async Task DeletePostAsync(Guid id, string partitionKey)
        {
            await this.postContainer.DeleteItemAsync<BirdPost>(id.ToString(), new PartitionKey(partitionKey));
        }

        public async Task UpdatePostAsync(BirdPost item)
        {
            await this.postContainer.UpsertItemAsync<BirdPost>(item, new PartitionKey(item.PostId.ToString()));
        }

        public async Task<BirdPost> GetPostAsync(Guid postId)
        {
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.postId = @PostId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@PostId", postId);
                var query = this.postContainer.GetItemQueryIterator<BirdPost>(queryDef);
                var response = await query.ReadNextAsync();

                return response.Resource.FirstOrDefault();
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<BirdComment>> GetCommentsAsync(Guid postId)
        {
            var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.postId = @PostId ORDER BY p.createDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@PostId", postId);
            var query = this.postContainer.GetItemQueryIterator<BirdComment>(queryDef);

            List<BirdComment> comments = new List<BirdComment>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                comments.AddRange(response.ToList());
            }

            return comments;
        }

        public async Task CreateCommentAsync(BirdComment comment)
        {
            await this.postContainer.CreateItemAsync<BirdComment>(comment, new PartitionKey(comment.PostId.ToString()));
        }

        public async Task DeleteCommentAsync(Guid commentId, System.Guid userId)
        {
            await this.postContainer.DeleteItemAsync<BirdComment>(commentId.ToString(), new PartitionKey(userId.ToString()));
        }
    }
}