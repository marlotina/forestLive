using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Birds.Configuration.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using FL.WebAPI.Core.Birds.Domain.Repository;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IBirdsConfiguration iBirdsConfiguration;
        private readonly Container birdContainer;
        private readonly ILogger<SpeciesRepository> iLogger;

        public SpeciesRepository(
            ILogger<SpeciesRepository> iLogger,
            IClientFactory iClientFactory,
            IBirdsConfiguration iBirdsConfiguration)
        {
            this.iLogger = iLogger;
            this.iClientFactory = iClientFactory;
            this.iBirdsConfiguration = iBirdsConfiguration;
            this.birdContainer = this.InitialClient();
        }

        private Container InitialClient()
        {
            var config = this.iBirdsConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosBirdContainer);
        }
        public async Task<List<PostDto>> GetPostsBySpecieAsync(Guid specieId, string orderCondition)
        {
            var posts = new List<PostDto>();
            try
            {
                var queryString = $"SELECT p.postId, p.title, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate, p.observationDate FROM p WHERE p.specieId = @SpecieId ORDER BY p.{orderCondition}";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@SpecieId", specieId);
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

        public async Task<List<PostHomeDto>> GetLastSpecieAsync()
        {
            var posts = new List<PostHomeDto>();
            try
            {
                var queryString = $"SELECT TOP 6 p.postId, p.title, p.specieName, p.specieId, p.imageUrl, p.altImage, p.userId, p.observationDate FROM p ORDER BY p.creationDate";

                var queryDef = new QueryDefinition(queryString);
                var query = this.birdContainer.GetItemQueryIterator<PostHomeDto>(queryDef);

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
