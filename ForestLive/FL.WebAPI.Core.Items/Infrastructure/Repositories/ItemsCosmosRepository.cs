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
    public class ItemsCosmosRepository : IItemsRepository
    {
        private IClientFactory clientFactory;
        private IItemConfiguration itemConfiguration; 
        private Container itemsContainer;

        public ItemsCosmosRepository(IClientFactory clientFactory,
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

        public async Task CreateItemAsync(Item item)
        {
            //await this._itemsContainer.UpsertItemAsync<Item>(item, new PartitionKey(item.ItemId));
            await this.itemsContainer.CreateItemAsync<Item>(item, new PartitionKey(item.ItemId));
        }

        public async Task DeleteItemAsync(System.Guid itemIdRequest)
        {
            var itemId = itemIdRequest.ToString();
            await this.itemsContainer.DeleteItemAsync<Item>(itemId, new PartitionKey(itemId.ToString()));
        }

        public async Task UpsertBlogPostAsync(Item item)
        {
            await this.itemsContainer.UpsertItemAsync<Item>(item, new PartitionKey(item.ItemId.ToString()));
        }

        public async Task<Item> GetItemAsync(Guid itemIdRequest)
        {
            try
            {
                var itemId = itemIdRequest.ToString();

                ItemResponse<Item> response = await this.itemsContainer.ReadItemAsync<Item>
                    (itemId, new PartitionKey(itemId));

                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<ItemComment>> GetItemCommentsAsync(Guid itemId)
        {
            var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.itemId = @ItemId ORDER BY p.dateCreated DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@ItemId", itemId);
            var query = this.itemsContainer.GetItemQueryIterator<ItemComment>(queryDef);

            List<ItemComment> comments = new List<ItemComment>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                comments.AddRange(response.ToList());
            }

            return comments;
        }

        public async Task CreateItemCommentAsync(ItemComment comment)
        {
            await this.itemsContainer.CreateItemAsync<ItemComment>(comment, new PartitionKey(comment.ItemId.ToString()));
        }

        public async Task DeleteCommentAsync(System.Guid commentId, System.Guid userId)
        {
            await this.itemsContainer.DeleteItemAsync<ItemComment>(commentId.ToString(), new PartitionKey(userId.ToString()));
        }
    }
}
