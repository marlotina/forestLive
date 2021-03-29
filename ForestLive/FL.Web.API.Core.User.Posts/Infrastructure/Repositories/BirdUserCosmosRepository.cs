using FL.CosmosDb.Standard.Contracts;
using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.User.Posts.Configuration.Contracts;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using FL.WebAPI.Core.User.Posts.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Infrastructure.Repositories
{
    public class BirdUserCosmosRepository : IBirdUserRepository
    {
        private IClientFactory iClientFactory;
        private IUserPostConfiguration iUserPostConfiguration;
        private Container usersContainer;

        public BirdUserCosmosRepository(IClientFactory iClientFactory,
            IUserPostConfiguration iUserPostConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iUserPostConfiguration = iUserPostConfiguration;
            this.usersContainer = InitialClient();
        }

        private Container InitialClient()
        {
            var config = this.iUserPostConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosUserContainer);
        }

        public async Task<List<PostDto>> GetPostsByUserAsync(string userId)
        {
            var posts = new List<PostDto>();

            var queryString = $"SELECT p.postId, p.title, p.type,  p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate FROM p WHERE p.type='post' AND p.userId = @UserId ORDER BY p.creationDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            var query = this.usersContainer.GetItemQueryIterator<PostDto>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }

        public async Task<List<PostDto>> GetAllByUserAsync(string userId)
        {
            var posts = new List<PostDto>();

            var queryString = $"SELECT p.postId, p.title, p.type, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate FROM p WHERE p.userId = @UserId ORDER BY p.creationDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            var query = this.usersContainer.GetItemQueryIterator<PostDto>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }

        public async Task<List<PointPostDto>> GetMapPointsByUserAsync(string userId)
        {
            var posts = new List<PointPostDto>();

            var queryString = $"SELECT p.postId, p.location FROM p WHERE p.location != null AND p.userId = @UserId ORDER BY p.creationDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            var query = this.usersContainer.GetItemQueryIterator<PointPostDto>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }

        public async Task<BirdPost> GetPostsAsync(Guid postId, string userId)
        {
            try
            {
                var response = await this.usersContainer.ReadItemAsync<BirdPost>(postId.ToString(), new PartitionKey(userId));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PostDto>> GetPostsByLabelAsync(string label, string userId)
        {
            var posts = new List<PostDto>();

            var queryString = $"SELECT p.postId, p.type, p.title, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate FROM p WHERE p.type='post' AND p.userId = @UserId AND ARRAY_CONTAINS(p.labels, @Label)";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            queryDef.WithParameter("@Label", label);
            var query = this.usersContainer.GetItemQueryIterator<PostDto>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }

        public async Task<List<PostDto>> GetBirdsBySpecieAsync(string userId, Guid specieId)
        {
            var posts = new List<PostDto>();

            var queryString = $"SELECT p.postId, p.type, p.title, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate FROM p WHERE p.type='bird' AND p.userId = @UserId AND p.specieId = @SpecieId ORDER BY p.creationDate";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            queryDef.WithParameter("@SpecieId", specieId);
            var query = this.usersContainer.GetItemQueryIterator<PostDto>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }

        public async Task<List<PostDto>> GetAllBirdsAsync(string userId)
        {
            var posts = new List<PostDto>();

            var queryString = $"SELECT p.postId, p.title, p.type, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate FROM p WHERE p.type='bird' AND p.userId = @UserId ORDER BY p.creationDate";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@UserId", userId);
            var query = this.usersContainer.GetItemQueryIterator<PostDto>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                posts.AddRange(response.ToList());
            }

            return posts;
        }
    }
}
