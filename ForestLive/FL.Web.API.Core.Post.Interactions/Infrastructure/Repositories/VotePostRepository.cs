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
    public class VotePostRepository : IVotePostRepository
    {
        private IClientFactory iClientFactory;
        private IPostConfiguration iVoteConfiguration;
        private Container voteContainer;

        public VotePostRepository(IClientFactory iClientFactory,
            IPostConfiguration iVoteConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iVoteConfiguration = iVoteConfiguration;
            this.voteContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.iVoteConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosVoteContainer);
        }

        public async Task<VotePost> GetVoteAsync(Guid voteId, Guid postId)
        {
            try
            {
                ItemResponse<VotePost> response = await this.voteContainer.ReadItemAsync<VotePost>(voteId.ToString(), new PartitionKey(postId.ToString()));
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
                result = await this.voteContainer.CreateItemAsync<VotePost>(votePost, new PartitionKey(votePost.PostId.ToString()));
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public async Task<bool> DeleteVoteAsync(Guid id, Guid postId)
        {
            try
            {
                await this.voteContainer.DeleteItemAsync<VotePost>(id.ToString(), new PartitionKey(postId.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<IEnumerable<VotePost>> GetVoteByPost(Guid postId)
        {
            //var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.userId = @UserId ORDER BY p.createDate ASC";
            var queryString = $"SELECT * FROM p WHERE p.postId = @PostId ORDER BY p.creationDate ASC";
            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@PostId", postId);
            var query = this.voteContainer.GetItemQueryIterator<VotePost>(queryDef);

            List<VotePost> votes = new List<VotePost>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                votes.AddRange(response.ToList());
            }

            return votes;
        }
    }
}
