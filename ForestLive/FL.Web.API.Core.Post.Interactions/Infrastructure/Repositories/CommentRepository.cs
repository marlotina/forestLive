using FL.CosmosDb.Standard.Contracts;
using FL.Web.API.Core.Post.Interactions.Configuration.Contracts;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using FL.Web.API.Core.Post.Interactions.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Infrastructure.Repositories
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

        public async Task<List<BirdComment>> GetCommentsByPostIdAsync(Guid postId)
        {
            //var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.userId = @UserId ORDER BY p.createDate ASC";
            var queryString = $"SELECT * FROM p WHERE p.postId = @PostId ORDER BY p.creationDate ASC";
            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@PostId", postId);
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

        public async Task<BirdComment> GetCommentAsync(Guid commentId, Guid postId)
        {
            try
            {
                ItemResponse<BirdComment> response = await this.commentContainer.ReadItemAsync<BirdComment>(commentId.ToString(), new PartitionKey(postId.ToString()));
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

                return await this.commentContainer.CreateItemAsync<BirdComment>(comment, new PartitionKey(comment.PostId.ToString()));
            }
            catch (Exception ex)
            {
            }

            return comment;
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId, Guid postId)
        {
            try
            {
                await this.commentContainer.DeleteItemAsync<BirdComment>(commentId.ToString(), new PartitionKey(postId.ToString()));
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task IncreaseVoteCommentCountAsync(Guid commentId, Guid postId)
        {
            try
            {
                var obj = new dynamic[] { commentId.ToString() };
                await this.commentContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseVoteCount", new PartitionKey(postId.ToString()), obj);
            }
            catch (Exception ex)
            {

            }

        }

        public async Task DecreaseVoteCommentCountAsync(Guid commentId, Guid postId)
        {
            try
            {
                var obj = new dynamic[] { commentId.ToString() };
                await this.commentContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCount", new PartitionKey(postId.ToString()), obj);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
