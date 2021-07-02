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
    public class VotePostRepository : IVotePostRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IPostConfiguration iVoteConfiguration;
        private Container voteContainer;
        private readonly ILogger<VotePostRepository> iLogger;

        public VotePostRepository(
            IClientFactory iClientFactory,
            ILogger<VotePostRepository> iLogger,
            IPostConfiguration iVoteConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iVoteConfiguration = iVoteConfiguration;
            this.voteContainer = InitialCLient();
            this.iLogger = iLogger;
        }

        private Container InitialCLient()
        {
            var config = this.iVoteConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosPostContainer);
        }

        public async Task<VotePost> GetVoteAsync(string voteId, Guid postId)
        {
            try
            {
                ItemResponse<VotePost> response = await this.voteContainer.ReadItemAsync<VotePost>(voteId, new PartitionKey(postId.ToString()));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<VotePost> AddVote(VotePost votePost)
        {
            var result = default(VotePost);
            try
            {
                result = await this.voteContainer.CreateItemAsync<VotePost>(votePost, new PartitionKey(votePost.PostId.ToString()));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                this.iLogger.LogError(ex.Message);
                throw;
            }

            return result;
        }

        public async Task<bool> DeleteVoteAsync(string id, Guid postId)
        {
            try
            {
                await this.voteContainer.DeleteItemAsync<VotePost>(id, new PartitionKey(postId.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
            
        }

        public async Task<IEnumerable<VotePost>> GetVoteByPostAsync(Guid postId)
        {
            var votes = new List<VotePost>();
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.type='vote' AND p.postId = @PostId ORDER BY p.creationDate ASC";
                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@PostId", postId);
                var query = this.voteContainer.GetItemQueryIterator<VotePost>(queryDef);

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
    }
}
