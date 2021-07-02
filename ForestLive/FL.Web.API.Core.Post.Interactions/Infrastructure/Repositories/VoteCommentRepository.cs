using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
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
    public class VoteCommentRepository : IVoteCommentRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IPostConfiguration iVoteConfiguration;
        private readonly ILogger<VotePostRepository> iLogger;
        private Container voteCommentContainer;

        public VoteCommentRepository(
            IClientFactory iClientFactory,
            ILogger<VotePostRepository> iLogger,
            IPostConfiguration iVoteConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iVoteConfiguration = iVoteConfiguration;
            this.voteCommentContainer = InitialCLient();
            this.iLogger = iLogger;
        }

        private Container InitialCLient()
        {
            var config = this.iVoteConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosPostContainer);
        }

        public async Task<VoteCommentPost> GetVoteAsync(string voteId, Guid postId)
        {
            try
            {
                ItemResponse<VoteCommentPost> response = await this.voteCommentContainer.ReadItemAsync<VoteCommentPost>(voteId, new PartitionKey(postId.ToString()));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteVoteAsync(string voteId, Guid postId)
        {
            try
            {
                await this.voteCommentContainer.DeleteItemAsync<VoteCommentPost>(voteId, new PartitionKey(postId.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<VoteCommentPost>> GetVoteByPostAsync(Guid postId)
        {     
            var votes = new List<VoteCommentPost>();
            try
            {
                //var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.userId = @UserId ORDER BY p.createDate ASC";
                var queryString = $"SELECT * FROM p WHERE p.type='voteComment' AND p.postId = @PostId ORDER BY p.creationDate ASC";
                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@PostId", postId);
                var query = this.voteCommentContainer.GetItemQueryIterator<VoteCommentPost>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    votes.AddRange(response.ToList());
                }

            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }

            return votes;
        }

        public async Task<VoteCommentPost> AddCommentVoteAsync(VoteCommentPost voteCommentPost)
        {
            try
            {
                return await this.voteCommentContainer.CreateItemAsync<VoteCommentPost>(voteCommentPost, new PartitionKey(voteCommentPost.PostId.ToString()));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                this.iLogger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteCommentVoteAsync(string voteId, Guid postId)
        {
            try
            {
                await this.voteCommentContainer.DeleteItemAsync<VotePost>(voteId, new PartitionKey(postId.ToString()));
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
