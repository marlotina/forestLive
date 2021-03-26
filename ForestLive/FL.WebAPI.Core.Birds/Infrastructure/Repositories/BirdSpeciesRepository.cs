using FL.CosmosDb.Standard.Contracts;
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
    public class BirdSpeciesRepository : IBirdSpeciesRepository
    {
        private IClientFactory clientFactory;
        private IBirdsConfiguration birdsConfiguration;
        private readonly Container birdContainer;

        public BirdSpeciesRepository(IClientFactory clientFactory,
            IBirdsConfiguration birdsConfiguration)
        {
            this.clientFactory = clientFactory;
            this.birdsConfiguration = birdsConfiguration;
            this.birdContainer = InitialClient();
        }

        private Container InitialClient()
        {
            var config = this.birdsConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosBirdContainer);
        }

        public async Task<List<PostDto>> GetBirdsPostsBySpecieId(string specieId, string orderCondition)
        {
            var posts = new List<PostDto>();
            try
            {
                var queryString = $"SELECT p.postId, p.title, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate FROM p WHERE p.specieId = @SpecieId ORDER BY p.{orderCondition}";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@SpecieId", Guid.Parse(specieId));
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
            }

            return posts;
        }
    }
}
