﻿using FL.CosmosDb.Standard.Contracts;
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
    public class BirdSpeciesRepository : IBirdSpeciesRepository
    {
        private IClientFactory clientFactory;
        private IBirdsConfiguration birdsConfiguration;
        private Container usersContainer;

        public BirdSpeciesRepository(IClientFactory clientFactory,
            IBirdsConfiguration birdsConfiguration)
        {
            this.clientFactory = clientFactory;
            this.birdsConfiguration = birdsConfiguration;
            this.usersContainer = InitialClient();
        }

        private Container InitialClient()
        {
            var config = this.birdsConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync();
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosUserContainer);
        }

        public async Task<List<BirdPost>> GetBirdsPostsBySpecieId(string specieId)
        {

            var blogPosts = new List<BirdPost>();


            var queryString = $"SELECT * FROM p WHERE p.type='post' AND p.specieId = @SpecieId ORDER BY p.createDate DESC";

            var queryDef = new QueryDefinition(queryString);
            queryDef.WithParameter("@SpecieId", specieId);
            var query = this.usersContainer.GetItemQueryIterator<BirdPost>(queryDef);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                var ru = response.RequestCharge;
                blogPosts.AddRange(response.ToList());
            }

            return blogPosts;
        }
    }
}
}
