using FL.CosmosDb.Standard.Contracts;
using FL.Web.Api.Core.Votes.Configuration.Contracts;
using FL.Web.Api.Core.Votes.Domain.Entities;
using FL.Web.Api.Core.Votes.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Infrastructure.Repositories
{
    public class VotePostRepository : IVotePostRepository
    {
        private IClientFactory clientFactory;
        private IVoteConfiguration voteConfiguration;
        private Container voteContainer;

        public VotePostRepository(IClientFactory clientFactory,
            IVoteConfiguration voteConfiguration)
        {
            this.clientFactory = clientFactory;
            this.voteConfiguration = voteConfiguration;
            this.voteContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.voteConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosVoteContainer);
        }

        public async Task<VotePost> GetVoteAsync(Guid voteId, string userId)
        {
            try
            {
                ItemResponse<VotePost> response = await this.voteContainer.ReadItemAsync<VotePost>(voteId.ToString(), new PartitionKey(userId.ToString()));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<VotePost> AddVote(VotePost votePost)
        {
            var result = default(VotePost);
            try
            {
                result = await this.voteContainer.CreateItemAsync<VotePost>(votePost, new PartitionKey(votePost.UserId.ToString()));
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public async Task<bool> DeleteVoteAsync(Guid id, string partitionKey)
        {
            try
            {
                await this.voteContainer.DeleteItemAsync<VotePost>(id.ToString(), new PartitionKey(partitionKey));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<List<VotePost>> GetVotePostAsync(List<Guid> listPost, string userId)
        {
            var votes = new List<VotePost>();

            try
            {
                var queryString = $"SELECT * FROM p WHERE p.userId = @UserId AND ARRAY_CONTAINS(@ListPost, p.postId) ORDER BY p.creationDate DESC";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                queryDef.WithParameter("@ListPost", listPost.ToArray());
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
            }

            return votes;
        }

        public async Task<List<VotePost>> GetVotesByUserId(string userId)
        {
            var blogPosts = new List<VotePost>();
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.type='vote' AND p.userId = @UserId ORDER BY p.createDate DESC";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.voteContainer.GetItemQueryIterator<VotePost>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    blogPosts.AddRange(response.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            return blogPosts;
        }
    }
}
