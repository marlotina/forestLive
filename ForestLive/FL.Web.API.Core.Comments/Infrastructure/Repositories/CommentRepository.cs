using FL.CosmosDb.Standard.Contracts;
using FL.Web.API.Core.Comments.Configuration.Contracts;
using FL.Web.API.Core.Comments.Domain.Entities;
using FL.Web.API.Core.Comments.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Comments.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private IClientFactory clientFactory;
        private IPostConfiguration itemConfiguration;
        private Container commentContainer;

        public CommentRepository(IClientFactory clientFactory,
            IPostConfiguration itemConfiguration)
        {
            this.clientFactory = clientFactory;
            this.itemConfiguration = itemConfiguration;
            this.commentContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.itemConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosCommentContainer);
        }

        public async Task<List<BirdComment>> GetCommentsByUserIdAsync(string userId)
        {
            //var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.userId = @UserId ORDER BY p.createDate ASC";
            var queryString = $"SELECT * FROM p WHERE p.userId = @UserId ORDER BY p.createDate ASC";
            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            var query = this.commentContainer.GetItemQueryIterator<BirdComment>(queryDef);

            List<BirdComment> comments = new List<BirdComment>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                comments.AddRange(response.ToList());
            }

            return comments;
        }

        public async Task<BirdComment> GetCommentAsync(Guid commentId, string userId)
        {
            try
            {
                ItemResponse<BirdComment> response = await this.commentContainer.ReadItemAsync<BirdComment>(commentId.ToString(), new PartitionKey(userId));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<BirdComment> CreateCommentAsync(BirdComment comment)
        {
            try
            {

                return await this.commentContainer.CreateItemAsync<BirdComment>(comment, new PartitionKey(comment.UserId.ToString()));
            }
            catch (Exception ex)
            {
            }

            return comment;
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId, string userId)
        {
            try
            {
                await this.commentContainer.DeleteItemAsync<BirdComment>(commentId.ToString(), new PartitionKey(userId));
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
