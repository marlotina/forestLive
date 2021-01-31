using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Entities.User;
using FL.WebAPI.Core.Items.Infrastructure.Services.Contracts;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Services.Implementations
{
    public class BlogCosmosDbService : IBlogCosmosDbService
    {

        private Container _usersContainer;
        private Container _itemsContainer;

        public BlogCosmosDbService(CosmosClient dbClient, string databaseName)
        {
            _usersContainer = dbClient.GetContainer(databaseName, "users");
            _itemsContainer = dbClient.GetContainer(databaseName, "birdItems");
        }

        public async Task CreateItemAsync(Item item)
        {
            //await this._itemsContainer.UpsertItemAsync<Item>(item, new PartitionKey(item.ItemId));
            await this._itemsContainer.CreateItemAsync<Item>(item, new PartitionKey(item.ItemId));
        }

        public async Task CreateItemCommentAsync(ItemComment comment)
        {
            //string str = JsonConvert.SerializeObject(comment);
            //dynamic obj = JsonConvert.DeserializeObject(str);

            var obj = new dynamic[] { comment.ItemId, comment };

            //var result = await _blogDbService.GetContainer("database", "container").Scripts.ExecuteStoredProcedureAsync<string>("spCreateToDoItem", new PartitionKey("Personal"), newItem);
            var result = await _itemsContainer.Scripts.ExecuteStoredProcedureAsync<string>("createComment", new PartitionKey(comment.ItemId.ToString()), obj);
            //await this._postsContainer.CreateItemAsync<BlogPostComment>(comment, new PartitionKey(comment.PostId));
        }

        public async Task CreateUserAsync(UserBird user)
        {
            await _usersContainer.CreateItemAsync<UserBird>(user, new PartitionKey(user.UserId.ToString()));
            //await _usersContainer.CreateItemAsync<BlogUser>(user, new PartitionKey(user.UserId), new ItemRequestOptions { PreTriggers = new List<string> { "validateUserUsernameNotExists" } });
        }

        public async Task<Item> GetItemAsync(string itemId)
        {
            try
            {
                //When getting the blogpost from the Posts container, the id is postId and the partitionKey is also postId.
                //  This will automatically return only the type="post" for this postId (and not the type=comment or any other types in the same partition postId)
                ItemResponse<Item> response = await this._itemsContainer.ReadItemAsync<Item>
                    (itemId, new Microsoft.Azure.Cosmos.PartitionKey(itemId));

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
            var query = this._itemsContainer.GetItemQueryIterator<ItemComment>(queryDef);

            List<ItemComment> comments = new List<ItemComment>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                comments.AddRange(response.ToList());
            }

            return comments;
        }

        public async Task UpsertBlogPostAsync(Item item)
        {
            await this._itemsContainer.UpsertItemAsync<Item>(item, new PartitionKey(item.ItemId.ToString()));
        }
    }
}
