using FL.WebAPI.Core.Items.Configuration.Contracts;
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
        private Container itemsContainer;

        public BirdPostCosmosRepository(IClientFactory clientFactory,
            IItemConfiguration itemConfiguration)
        {
            this.clientFactory = clientFactory;
            this.itemConfiguration = itemConfiguration;
            this.itemsContainer = InitialCLient();
        }

        private Container InitialCLient() {
            var config = this.itemConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync();
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosBirdContainer);
        }

        public async Task CreateItemAsync(BirdPost item)
        {
            await this.itemsContainer.CreateItemAsync<BirdPost>(item, new PartitionKey(item.ItemId.ToString()));
        }

        public async Task DeleteItemAsync(Guid id, string partitionKey)
        {
            await this.itemsContainer.DeleteItemAsync<BirdPost>(id.ToString(), new PartitionKey(partitionKey));
        }

        public async Task UpsertBlogPostAsync(BirdPost item)
        {
            await this.itemsContainer.UpsertItemAsync<BirdPost>(item, new PartitionKey(item.ItemId.ToString()));
        }

        public async Task<BirdPost> GetItemAsync(Guid itemIdRequest)
        {
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.itemId = @ItemId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@ItemId", itemIdRequest);
                var query = this.itemsContainer.GetItemQueryIterator<BirdPost>(queryDef);
                var response = await query.ReadNextAsync();

                return response.Resource.FirstOrDefault();
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<BirdComment>> GetItemCommentsAsync(Guid itemId)
        {
            var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.itemId = @ItemId ORDER BY p.createDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@ItemId", itemId);
            var query = this.itemsContainer.GetItemQueryIterator<BirdComment>(queryDef);

            List<BirdComment> comments = new List<BirdComment>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                comments.AddRange(response.ToList());
            }

            return comments;
        }

        public async Task CreateItemCommentAsync(BirdComment comment)
        {
            await this.itemsContainer.CreateItemAsync<BirdComment>(comment, new PartitionKey(comment.ItemId.ToString()));
        }

        public async Task DeleteCommentAsync(Guid commentId, System.Guid userId)
        {
            await this.itemsContainer.DeleteItemAsync<BirdComment>(commentId.ToString(), new PartitionKey(userId.ToString()));
        }
    }
}
