using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Bird.Pending.Configuration.Contracts;
using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using FL.Web.API.Core.Bird.Pending.Domain.Repository;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Infrastructure.Repositories
{
    public class BirdPendingRepository : IBirdPendingRepository
    {
        private IClientFactory iClientFactory;
        private readonly IBirdPendingConfiguration iBirdPendingConfiguration;
        private readonly Container birdContainer;
        private readonly ILogger<BirdPendingRepository> iLogger;

        public BirdPendingRepository(
            IClientFactory iClientFactory,
            ILogger<BirdPendingRepository> iLogger,
            IBirdPendingConfiguration iBirdPendingConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iBirdPendingConfiguration = iBirdPendingConfiguration;
            this.birdContainer = this.InitialClient();
            this.iLogger = iLogger;
        }

        private Container InitialClient()
        {
            var config = this.iBirdPendingConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosBirdPendingContainer);
        }

        public async Task<List<PostDto>> GetAllSpecieAsync(string orderCondition)
        {
            var posts = new List<PostDto>();
            try
            {
                var queryString = $"SELECT p.postId, p.title, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate, p.observationDate FROM p ORDER BY p.{orderCondition}";

                var queryDef = new QueryDefinition(queryString);
                var query = this.birdContainer.GetItemQueryIterator<PostDto>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    posts.AddRange(response.ToList());
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }

            return posts;
        }
    }
}
