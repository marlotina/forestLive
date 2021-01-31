using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.Services.Contracts;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Services.Implementations
{
    public class ItemsCosmosRepository : IItemsCosmosRepository
    {
        private IClientFactory clientFactory;
        private IItemConfiguration itemConfiguration; 
        private Container itemsContainer;

        public ItemsCosmosRepository(IClientFactory clientFactory,
            IItemConfiguration itemConfiguration)
        {
            this.clientFactory = clientFactory;
            this.itemConfiguration = itemConfiguration;
            this.itemsContainer = InitialCLient();// dbClient.GetContainer(databaseName, "birdItems");
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

        public async Task UpsertBlogPostAsync(Item item)
        {
            await this.itemsContainer.UpsertItemAsync<Item>(item, new PartitionKey(item.ItemId.ToString()));
        }

        public async Task<Item> GetItemAsync(string itemId)
        {
            try
            {
                //When getting the blogpost from the Posts container, the id is postId and the partitionKey is also postId.
                //  This will automatically return only the type="post" for this postId (and not the type=comment or any other types in the same partition postId)
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

        public async Task<List<ItemComment>> GetItemCommentsAsync(string postId)
        {
            var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.postId = @PostId ORDER BY p.dateCreated DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@PostId", postId);
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
            //string str = JsonConvert.SerializeObject(comment);
            //dynamic obj = JsonConvert.DeserializeObject(str);

            var obj = new dynamic[] { comment.ItemId, comment };

            //var result = await _blogDbService.GetContainer("database", "container").Scripts.ExecuteStoredProcedureAsync<string>("spCreateToDoItem", new PartitionKey("Personal"), newItem);
            var result = await this.itemsContainer.Scripts.ExecuteStoredProcedureAsync<string>("createComment", new PartitionKey(comment.ItemId.ToString()), obj);
            //await this._postsContainer.CreateItemAsync<BlogPostComment>(comment, new PartitionKey(comment.PostId));
        }

        Task<List<ItemComment>> IItemsCosmosRepository.GetItemCommentsAsync(string postId)
        {
            throw new System.NotImplementedException();
        }
    }
}
