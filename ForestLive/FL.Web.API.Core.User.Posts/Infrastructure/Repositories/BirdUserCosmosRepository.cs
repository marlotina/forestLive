using FL.CosmosDb.Standard.Contracts;
using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.User.Posts.Configuration.Contracts;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using FL.WebAPI.Core.User.Posts.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<PostDto>> GetUserPosts(string userId, string label, string type)
        {
            var posts = new List<PostDto>();
            var queryString = new StringBuilder();
            var parameters = new Dictionary<string, string>();
            queryString.Append("SELECT p.postId, p.title, p.type,  p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate, p.observationDate FROM p WHERE p.userId = @UserId ");
            parameters.Add("@UserId", userId);

            if (!string.IsNullOrEmpty(type) && type != "all") {
                queryString.Append("AND p.type=@Type ");
                parameters.Add("@Type", type);
            }

            if (label != "none")
            {
                queryString.Append("AND ARRAY_CONTAINS(p.labels, @Label) ");
                parameters.Add("@Label", label);
            }

            queryString.Append("ORDER BY p.creationDate DESC");

            //var queryString = $"SELECT p.postId, p.title, p.type,  p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate, p.observationDate FROM p WHERE p.type='post' AND p.userId = @UserId ORDER BY p.creationDate DESC";

            var queryDef = new QueryDefinition(queryString.ToString());

            foreach (var param in parameters)
            {
                queryDef.WithParameter(param.Key, param.Value);
            }
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
