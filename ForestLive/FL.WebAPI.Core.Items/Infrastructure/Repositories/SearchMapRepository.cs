using FL.CosmosDb.Standard.Contracts;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class SearchMapRepository : ISearchMapRepository
    {

        private IClientFactory clientFactory;
        private IPostConfiguration postConfiguration;
        private Container postContainer;

        public SearchMapRepository(IClientFactory clientFactory,
            IPostConfiguration postConfiguration)
        {
            this.clientFactory = clientFactory;
            this.postConfiguration = postConfiguration;
            this.postContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.postConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosPostContainer);
        }

        public async Task<List<BirdPost>> GetPostByRadio(double latitude, double longitude, int meters)
        {
            try 
            {
                var queryString = @"SELECT * FROM p WHERE p.type='post' AND ST_DISTANCE(p.location, {'type': 'Point', 'coordinates':[@Longitude, @Latitude]}) < @Distance";

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
    }
}
