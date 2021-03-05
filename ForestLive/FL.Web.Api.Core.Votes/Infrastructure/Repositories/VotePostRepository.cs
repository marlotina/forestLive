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
        private IVoteConfiguration itemConfiguration;
        private Container postContainer;

        public VotePostRepository(IClientFactory clientFactory,
            IVoteConfiguration itemConfiguration)
        {
            this.clientFactory = clientFactory;
            this.itemConfiguration = itemConfiguration;
            this.postContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.itemConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync();
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosVoteContainer);
        }

        public async Task<VotePost> AddVotePost(VotePost votePost)
        {
            try
            {
                var obj = new dynamic[] { votePost.PostId, votePost };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<VotePost>("createVote", new PartitionKey(votePost.PostId.ToString()), obj);
            }
            catch (Exception es)
            {
            }

            return votePost;
            //return await this.postContainer.CreateItemAsync<VotePost>(votePost, new PartitionKey(votePost.PostId.ToString()));
        }
    }
}
