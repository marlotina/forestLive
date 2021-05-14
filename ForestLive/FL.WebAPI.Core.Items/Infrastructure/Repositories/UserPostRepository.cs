using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repository;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Infrastructure.Repositories
{
    public class UserPostRepository : IUserPostRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IPostConfiguration iPostConfiguration;
        private readonly Container userContainer;
        private readonly ILogger<UserPostRepository> iLogger;

        public UserPostRepository(
            ILogger<UserPostRepository> iLogger,
            IClientFactory iClientFactory,
            IPostConfiguration iPostConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iPostConfiguration = iPostConfiguration;
            this.iLogger = iLogger;
            this.userContainer = this.InitialClient();
        }

        private Container InitialClient()
        {
            var config = this.iPostConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosUserContainer);
        }

        public async Task<bool> CreatePostAsync(BirdPost post)
        {
            try
            {
                await this.userContainer.CreateItemAsync<BirdPost>(post, new PartitionKey(post.SpecieId.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeletePostAsync(Guid postId, Guid specieId)
        {
            try
            {
                await this.userContainer.DeleteItemAsync<BirdPost>(postId.ToString(), new PartitionKey(specieId.ToString()));
                return true;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
            }

            return false;

        }
    }
}
