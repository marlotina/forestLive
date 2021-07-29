using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Post.Interactions.Configuration.Contracts;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
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
        private readonly IClientFactory iClientFactory;
        private readonly IPostConfiguration iItemConfiguration;
        private readonly ILogger<VotePostRepository> iLogger;
        private Container commentContainer;

        public CommentRepository(
            IClientFactory iClientFactory,
            ILogger<VotePostRepository> iLogger,
            IPostConfiguration iItemConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iItemConfiguration = iItemConfiguration;
            this.commentContainer = InitialCLient();
            this.iLogger = iLogger;
        }

        private Container InitialCLient()
        {
            var config = this.iItemConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosPostContainer);
        }

        public async Task<List<PostDetails>> GetCommentsByPostIdAsync(Guid postId, string userId)
        {
            var comments = new List<PostDetails>();
            try
            {
                var queryString = $"SELECT c.id, c.type, c.postId, c.userId, c.creationDate, c.commentId, c.text, c.voteCount, c.parentId FROM c WHERE c.postId = @PostId AND c.type='comment' OR  (c.type='vote' AND c.userId = @UserId) OR (c.type='voteComment' AND c.userId = @UserId)";
                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@PostId", postId);
                queryDef.WithParameter("@UserId", userId);
                var query = this.commentContainer.GetItemQueryIterator<PostDetails>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    comments.AddRange(response.ToList());
                }
            }
            catch (Exception ex) 
            {
                this.iLogger.LogError(ex.Message);
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
                this.iLogger.LogError(ex.Message);
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
                this.iLogger.LogError(ex.Message);
            }

            return comment;
        }

        public async Task<bool> DeleteCommentAsync(BirdComment comment)
        {
            try
            {
                await this.commentContainer.UpsertItemAsync<BirdComment>(comment, new PartitionKey(comment.PostId.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> IncreaseVoteCommentCountAsync(Guid commentId, Guid postId)
        {
            try
            {
                var obj = new dynamic[] { commentId.ToString() };
                await this.commentContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseVoteCommentCount", new PartitionKey(postId.ToString()), obj);
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }

        }

        public async Task<bool> DecreaseVoteCommentCountAsync(Guid commentId, Guid postId)
        {
            try
            {
                var obj = new dynamic[] { commentId.ToString() };
                await this.commentContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCommentCount", new PartitionKey(postId.ToString()), obj);
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }
    }
}
