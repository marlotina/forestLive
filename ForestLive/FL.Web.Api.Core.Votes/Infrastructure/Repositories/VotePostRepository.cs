using FL.CosmosDb.Standard.Contracts;
using FL.Web.Api.Core.Votes.Configuration.Contracts;
using FL.Web.Api.Core.Votes.Domain.Entities;
using FL.Web.Api.Core.Votes.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
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
                //var obj = new dynamic[] { votePost.PostId, votePost };
                //var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<VotePost>("createVote", new PartitionKey(votePost.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }

            return result;
            //return await this.postContainer.CreateItemAsync<VotePost>(votePost, new PartitionKey(votePost.PostId.ToString()));
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
    }
}
