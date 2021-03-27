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
        private IClientFactory iClientFactory;
        private IBirdsConfiguration iBirdsConfiguration;
        private readonly Container birdContainer;

        public BirdSpeciesRepository(IClientFactory iClientFactory,
            IBirdsConfiguration iBirdsConfiguration)
        {
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

        public async Task<BirdPost> CreatePostAsync(BirdPost post)
        {
            return await this.birdContainer.CreateItemAsync<BirdPost>(post, new PartitionKey(post.SpecieId.ToString()));
        }

        public async Task<List<PostDto>> GetPostsBySpecieAsync(Guid specieId, string orderCondition)
        {
            var posts = new List<PostDto>();
            try
            {
                var queryString = $"SELECT p.postId, p.title, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate FROM p WHERE p.specieId = @SpecieId ORDER BY p.{orderCondition}";

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
            }

            return posts;
        }

        public async Task<BirdPost> GetPostsAsync(Guid postId, Guid specieId)
        {
            try
            {
                var response = await this.birdContainer.ReadItemAsync<BirdPost>(postId.ToString(), new PartitionKey(specieId.ToString()));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task DeletePostAsync(Guid postId, Guid specieId)
        {
            await this.birdContainer.DeleteItemAsync(postId.ToString(), new PartitionKey(specieId.ToString()));
        }
    }
}
