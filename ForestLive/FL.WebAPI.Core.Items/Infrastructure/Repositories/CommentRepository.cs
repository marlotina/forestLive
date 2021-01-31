using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IItemConfiguration itemConfiguration;

        public CommentRepository(IItemConfiguration itemConfiguration)
        {
            this.itemConfiguration = itemConfiguration;
        }

        public async Task<Comment> AddComment(Comment comment)
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

                var commentContainer = client.GetContainer(this.itemConfiguration.CosmosDatabaseId, this.itemConfiguration.CosmosContainerId);
                return await commentContainer.CreateItemAsync(comment);
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
            }

            return comment;
        }

        public async Task<bool> DeleteComment(Guid idComment)
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

                var container = client.GetContainer(this.itemConfiguration.CosmosDatabaseId, this.itemConfiguration.CosmosContainerId);

                // Delete an item. Note we must provide the partition key value and id of the item to delete
                var wop = await container.DeleteItemAsync<BirdItem>(idComment.ToString(), new PartitionKey("commentId"));
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
