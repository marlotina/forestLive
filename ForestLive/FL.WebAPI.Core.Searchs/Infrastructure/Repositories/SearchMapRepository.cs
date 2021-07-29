using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Searchs.Configuration.Contracts;
using FL.WebAPI.Core.Searchs.Domain.Repository;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Infrastructure.Repositories
{
    public class SearchMapRepository : ISearchMapRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IBirdsConfiguration iBirdsConfiguration;
        private readonly ILogger<SearchMapRepository> iLogger;
        private Container postContainer;

        public SearchMapRepository(
            ILogger<SearchMapRepository> iLogger,
            IClientFactory iClientFactory,
            IBirdsConfiguration iBirdsConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iBirdsConfiguration = iBirdsConfiguration;
            this.iLogger = iLogger;
            this.postContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.iBirdsConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosBirdContainer);
        }

        public async Task<List<PointPostDto>> GetPostByRadio(double latitude, double longitude, int meters)
        {
            try 
            {
                var queryString = @"SELECT p.postId, p.location FROM p WHERE ST_DISTANCE(p.location, {'type': 'Point', 'coordinates':[@Longitude, @Latitude]}) < @Distance";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@Latitude", latitude);
                queryDef.WithParameter("@Longitude", longitude);
                queryDef.WithParameter("@Distance", meters);
                var query = this.postContainer.GetItemQueryIterator<PointPostDto>(queryDef);

                List<PointPostDto> posts = new List<PointPostDto>();
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
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<PointPostDto>> GetSpeciePostByRadio(double latitude, double longitude, int meters, Guid specieId)
        {
            var posts = new List<PointPostDto>();
            try
            {
                var queryString = @"SELECT p.postId, p.location FROM p WHERE p.specieId = @SpecieId  AND ST_DISTANCE(p.location, {'type': 'Point', 'coordinates':[@Longitude, @Latitude]}) < @Distance";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@Latitude", latitude);
                queryDef.WithParameter("@Longitude", longitude);
                queryDef.WithParameter("@Distance", meters);
                queryDef.WithParameter("@SpecieId", specieId.ToString());

                var query = this.postContainer.GetItemQueryIterator<PointPostDto>(queryDef);

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
