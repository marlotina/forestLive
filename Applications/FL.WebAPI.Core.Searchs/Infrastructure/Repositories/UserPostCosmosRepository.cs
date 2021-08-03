using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Searchs.Configuration.Contracts;
using FL.WebAPI.Core.Searchs.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Model;
using FL.WebAPI.Core.Searchs.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Infrastructure.Repositories
{
    public class UserPostCosmosRepository : IUserPostRepository
    {
        private IClientFactory iClientFactory;
        private IBirdsConfiguration iBirdsConfiguration;
        private Container usersContainer;
        private readonly ILogger<UserPostCosmosRepository> iLogger;

        public UserPostCosmosRepository(
            ILogger<UserPostCosmosRepository> iLogger,
            IClientFactory iClientFactory,
            IBirdsConfiguration iBirdsConfiguration)
        {
            this.iLogger = iLogger;
            this.iClientFactory = iClientFactory;
            this.iBirdsConfiguration = iBirdsConfiguration;
            this.usersContainer = InitialClient();
        }

        private Container InitialClient()
        {
            var config = this.iBirdsConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosUserContainer);
        }

        public async Task<List<PointPostDto>> GetMapPointsByUserAsync(string userId)
        {
            var posts = new List<PointPostDto>();

            try
            {
                var queryString = $"SELECT p.postId, p.location, p.userId FROM p WHERE p.location != null AND p.userId = @UserId ORDER BY p.creationDate DESC";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.usersContainer.GetItemQueryIterator<PointPostDto>(queryDef);

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

        public async Task<IEnumerable<PostDto>> GetUserPosts(string userId, string label, string type)
        {
            var posts = new List<PostDto>();

            try
            {
                var queryString = new StringBuilder();
                var parameters = new Dictionary<string, string>();
                queryString.Append("SELECT p.postId, p.title, p.type, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate, p.observationDate FROM p WHERE p.userId = @UserId ");
                parameters.Add("@UserId", userId);

                if (!string.IsNullOrEmpty(type) && type != "all")
                {
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
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }

            return posts;
        }

        public async Task<BirdPost> GetPostAsync(Guid birdPostId, string userId)
        {
            try
            {
                var response = await this.usersContainer.ReadItemAsync<BirdPost>(birdPostId.ToString(), new PartitionKey(userId));
                var ru = response.RequestCharge;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }
    }
}
