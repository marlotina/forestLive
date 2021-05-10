using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Configuration.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IVoteConfiguration iItemConfiguration;
        private readonly ILogger<CommentRepository> iLogger;
        private Container commentContainer;

        public CommentRepository(
            IClientFactory iClientFactory,
            IVoteConfiguration iItemConfiguration,
            ILogger<CommentRepository> iLogger)
        {
            this.iClientFactory = iClientFactory;
            this.iItemConfiguration = iItemConfiguration;
            this.iLogger = iLogger;
            this.commentContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.iItemConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosCommentContainer);
        }

        public async Task<List<CommentPost>> GetCommentsByUserIdAsync(string userId)
        {
            var comments = new List<CommentPost>();
            try
            {
                //var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.userId = @UserId ORDER BY p.createDate ASC";
                var queryString = $"SELECT p.id, p.text, p.specieId, p.creationDate, p.postId, p.authorPostId, p.titlePost FROM p WHERE p.userId = @UserId ORDER BY p.creationDate ASC";
                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.commentContainer.GetItemQueryIterator<CommentPost>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    comments.AddRange(response.ToList());
                }
            }
            catch (Exception ex) 
            {
                this.iLogger.LogError(ex);
            }            

            return comments;
        }
    }
}
