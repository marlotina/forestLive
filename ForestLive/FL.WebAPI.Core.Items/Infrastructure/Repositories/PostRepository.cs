﻿using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IItemConfiguration itemConfiguration; 
        public PostRepository(IItemConfiguration itemConfiguration)
        {
            this.itemConfiguration = itemConfiguration;
        }

        public async Task<BirdPost> AddBirdPost(BirdPost birdPost)
        {
            try
            {
                var connectionString = this.itemConfiguration.CosmosdbConnectionstring;

                var client = new CosmosClientBuilder(connectionString)
                                    .WithSerializerOptions(new CosmosSerializationOptions
                                    {
                                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                                    })
                                    .Build();

                var postContainer = client.GetContainer(this.itemConfiguration.CosmosDatabaseId, this.itemConfiguration.CosmosContainerId);
                return await postContainer.CreateItemAsync(birdPost);
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
            }

            return birdPost;
        }
    }
}
