﻿using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Birds.Configuration.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using FL.WebAPI.Core.Birds.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Infrastructure.Repositories
{
    public class PostCosmosRepository : IPostRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IBirdsConfiguration iBirdsConfiguration;
        private readonly ILogger<PostCosmosRepository> iLogger;
        private Container postContainer;

        public PostCosmosRepository(
            ILogger<PostCosmosRepository> iLogger,
            IClientFactory iClientFactory,
            IBirdsConfiguration iBirdsConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iBirdsConfiguration = iBirdsConfiguration;
            this.postContainer = InitialCLient();
            this.iLogger = iLogger;
        }

        private Container InitialCLient() {
            var config = this.iBirdsConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosPostContainer);
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
                this.iLogger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<PostDto>> GetPostsAsync(string orderBy)
        {
            var posts = new List<PostDto>();

            try
            {

                var queryString = $"SELECT p.postId, p.title, p.text, p.specieName, p.specieId, p.imageUrl, p.altImage, p.labels, p.commentCount, p.voteCount, p.userId, p.creationDate FROM p WHERE p.type='post' ORDER BY p.{orderBy}";

                var queryDef = new QueryDefinition(queryString);
                var query = this.postContainer.GetItemQueryIterator<PostDto>(queryDef);

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
            }

            return posts;
        }
    }
}