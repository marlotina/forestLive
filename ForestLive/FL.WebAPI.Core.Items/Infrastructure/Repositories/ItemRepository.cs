using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IItemConfiguration itemConfiguration; 
        public ItemRepository(IItemConfiguration itemConfiguration)
        {
            this.itemConfiguration = itemConfiguration;
        }

        public async Task<BirdItem> AddBirdItem(BirdItem birdItem)
        {
            try
            {
                var connectionString = this.itemConfiguration.CosmosdbConnectionstring;

                var client = new CosmosClientBuilder(connectionString)
                                    .WithSerializerOptions(new CosmosSerializationOptions
                                    {
                                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                                    })
                                    .Build();

                var postContainer = client.GetContainer(this.itemConfiguration.CosmosDatabaseId, this.itemConfiguration.CosmosContainerId);
                return await postContainer.CreateItemAsync(birdItem);
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
            }

            return birdItem;
        }

        public async Task<bool> DeleteBirdPost(Guid idItem)
        {
            try
            {
                var connectionString = this.itemConfiguration.CosmosdbConnectionstring;

                var client = new CosmosClientBuilder(connectionString)
                                    .WithSerializerOptions(new CosmosSerializationOptions
                                    {
                                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                                    })
                                    .Build();

                var postContainer = client.GetContainer(this.itemConfiguration.CosmosDatabaseId, this.itemConfiguration.CosmosContainerId);

                // Delete an item. Note we must provide the partition key value and id of the item to delete
                var wop = await postContainer.DeleteItemAsync<BirdItem>(idItem.ToString(), new PartitionKey("postId"));
                return true;
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
            }

            return false;
        }
    }
}
