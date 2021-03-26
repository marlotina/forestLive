using FL.CosmosDb.Standard.Contracts;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Dto;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class PostCosmosRepository : IPostRepository
    {
        private IClientFactory clientFactory;
        private IPostConfiguration postConfiguration; 
        private Container postContainer;

        public PostCosmosRepository(IClientFactory clientFactory,
            IPostConfiguration postConfiguration)
        {
            this.clientFactory = clientFactory;
            this.postConfiguration = postConfiguration;
            this.postContainer = InitialCLient();
        }

        private Container InitialCLient() {
            var config = this.postConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosPostContainer);
        }

        public async Task<BirdPost> CreatePostAsync(BirdPost post)
        {
            return await this.postContainer.CreateItemAsync<BirdPost>(post, new PartitionKey(post.PostId.ToString()));
        }

        public async Task DeletePostAsync(Guid id, string partitionKey)
        {
            await this.postContainer.DeleteItemAsync<BirdPost>(id.ToString(), new PartitionKey(partitionKey));
        }

        public async Task UpdatePostAsync(BirdPost post)
        {
            await this.postContainer.UpsertItemAsync<BirdPost>(post, new PartitionKey(post.PostId.ToString()));
        }

        public async Task<BirdPost> GetPostAsync(Guid postId)
        {
            try
            {
                var response = await this.postContainer.ReadItemAsync<BirdPost>(postId.ToString(), new PartitionKey(postId.ToString()));
                var ru = response.RequestCharge;
                return response.Resource;            
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<BirdComment>> GetCommentsAsync(Guid postId)
        {
            var queryString = $"SELECT * FROM p WHERE p.type='comment' AND p.postId = @PostId ORDER BY p.creationDate ASC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@PostId", postId);
            var query = this.postContainer.GetItemQueryIterator<BirdComment>(queryDef);

            List<BirdComment> comments = new List<BirdComment>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                comments.AddRange(response.ToList());
            }

            return comments;
        }

        public async Task<List<PostDto>> GetPostsAsync(string orderBy)
        {
            var queryString = $"SELECT p.postId, p.title, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate FROM p WHERE p.type='post' AND p.specieId = null ORDER BY p.{orderBy}";

            var queryDef = new QueryDefinition(queryString);
            var query = this.postContainer.GetItemQueryIterator<PostDto>(queryDef);

            var posts = new List<PostDto>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }

        public async Task<List<PostDto>> GetAllPostsAsync(string orderBy)
        {
            var queryString = $"SELECT p.postId, p.title, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate FROM p WHERE p.type='post' AND p.specieId != null ORDER BY p.{orderBy}";

            var queryDef = new QueryDefinition(queryString);
            var query = this.postContainer.GetItemQueryIterator<PostDto>(queryDef);

            var posts = new List<PostDto>();
            double wop = 0;
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                wop = wop + ru;
                posts.AddRange(response.ToList());
            }

            return posts;
        }
    }
}
