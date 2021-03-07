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
        private Container postContainer;

        public CommentRepository(IClientFactory clientFactory,
            IPostConfiguration itemConfiguration)
        {
            this.clientFactory = clientFactory;
            this.itemConfiguration = itemConfiguration;
            this.postContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.itemConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync();
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosBirdContainer);
        }

        public async Task<List<BirdComment>> GetCommentsAsync(string userId)
        {
            var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.postId = @PostId ORDER BY p.createDate ASC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@PostId", userId);
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

        public async Task<BirdComment> GetCommentAsync(Guid commentId, Guid postId)
        {
            BirdComment comment = new BirdComment();
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.id = @CommentId AND p.postId = @PostId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@PostId", postId);
                queryDef.WithParameter("@CommentId", commentId);
                var query = this.postContainer.GetItemQueryIterator<BirdComment>(queryDef);

                var response = await query.ReadNextAsync();
                return response.FirstOrDefault();
            }
            catch (Exception es)
            {
            }

            return comment;
        }

        public async Task<BirdComment> CreateCommentAsync(BirdComment comment)
        {
            try
            {
                var obj = new dynamic[] { comment.PostId, comment };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<BirdComment>("createComment", new PartitionKey(comment.PostId.ToString()), obj);
            }
            catch (Exception es)
            {
            }

            return comment;
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId, Guid postId)
        {
            try
            {
                var obj = new dynamic[] { postId, commentId };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<string>("deleteComment", new PartitionKey(postId.ToString()), obj);
            }
            catch (Exception es)
            {
                return false;
            }

            return true;
        }
    }
}
