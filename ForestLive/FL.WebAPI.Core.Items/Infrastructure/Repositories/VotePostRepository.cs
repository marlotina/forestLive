using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Contracts;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
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
