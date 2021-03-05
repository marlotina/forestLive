using FL.CosmosDb.Standard.Contracts;
using FL.Web.Api.Core.Votes.Configuration.Contracts;
using FL.Web.Api.Core.Votes.Domain.Entities;
using FL.Web.Api.Core.Votes.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Infrastructure.Repositories
{
    public class VotePostRepository : IVotePostRepository
    {
        private IClientFactory clientFactory;
        private IPostConfiguration itemConfiguration;
        private Container postContainer;

        public VotePostRepository(IClientFactory clientFactory,
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

        public async Task<VotePost> AddVotePost(VotePost votePost)
        {
            return await this.postContainer.CreateItemAsync<VotePost>(votePost, new PartitionKey(votePost.PostId.ToString()));
        }
    }
}
