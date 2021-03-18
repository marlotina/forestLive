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
        private IClientFactory clientFactory;
        private IBirdsConfiguration birdsConfiguration;
        private Container postContainer;

        public SearchMapRepository(IClientFactory clientFactory,
            IBirdsConfiguration birdsConfiguration)
        {
            this.clientFactory = clientFactory;
            this.birdsConfiguration = birdsConfiguration;
            this.postContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.birdsConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosBirdContainer);
        }

        public async Task<List<BirdPost>> GetPostByRadio(double latitude, double longitude, int meters)
        {
            try 
            {
                var queryString = @"SELECT p.postId, p.location FROM p WHERE p.type='post' AND ST_DISTANCE(p.location, {'type': 'Point', 'coordinates':[@Longitude, @Latitude]}) < @Distance";

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

        public async Task<BirdPost> GetPostsByPostId(string postId, string userId)
        {
            try
            {
                var response = await this.postContainer.ReadItemAsync<BirdPost>(postId.ToString(), new PartitionKey(userId));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
