using FL.CosmosDb.Standard.Contracts;
using FL.WebAPI.Core.Birds.Configuration.Contracts;
using FL.WebAPI.Core.Birds.Domain.Model;
using FL.WebAPI.Core.Birds.Domain.Repository;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Infrastructure.Repositories
{
    public class SearchMapRepository : ISearchMapRepository
    {
        private IClientFactory iClientFactory;
        private IBirdsConfiguration iBirdsConfiguration;
        private Container postContainer;

        public SearchMapRepository(IClientFactory iClientFactory,
            IBirdsConfiguration iBirdsConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iBirdsConfiguration = iBirdsConfiguration;
            this.postContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.iBirdsConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosBirdContainer);
        }

        public async Task<List<BirdPost>> GetPostByRadio(double latitude, double longitude, int meters)
        {
            try 
            {
                var queryString = @"SELECT p.postId, p.location, p.specieId FROM p WHERE p.type='post' AND ST_DISTANCE(p.location, {'type': 'Point', 'coordinates':[@Longitude, @Latitude]}) < @Distance";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@Latitude", latitude);
                queryDef.WithParameter("@Longitude", longitude);
                queryDef.WithParameter("@Distance", meters);
                var query = this.postContainer.GetItemQueryIterator<BirdPost>(queryDef);

                List<BirdPost> posts = new List<BirdPost>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    posts.AddRange(response.ToList());
                }

                return posts;
            }
            catch (Exception ex) 
            { 
            }
            return null;
        }

        public async Task<BirdPost> GetPostsByPostId(Guid postId, Guid specieId)
        {
            try
            {
                var response = await this.postContainer.ReadItemAsync<BirdPost>(postId.ToString(), new PartitionKey(specieId.ToString()));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<BirdPost>> GetSpeciePostByRadio(double latitude, double longitude, int meters, Guid specieId)
        {
            try
            {
                var queryString = @"SELECT p.postId, p.location, p.specieId FROM p WHERE p.type='post' AND p.specieId = @SpecieId  AND ST_DISTANCE(p.location, {'type': 'Point', 'coordinates':[@Longitude, @Latitude]}) < @Distance";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@Latitude", latitude);
                queryDef.WithParameter("@Longitude", longitude);
                queryDef.WithParameter("@Distance", meters);
                queryDef.WithParameter("@SpecieId", specieId);

                var query = this.postContainer.GetItemQueryIterator<BirdPost>(queryDef);

                List<BirdPost> posts = new List<BirdPost>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    posts.AddRange(response.ToList());
                }

                return posts;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
