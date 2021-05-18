using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Configuration.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Infrastructure.Repositories
{
    public class VotePostRepository : IVotePostRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IVoteConfiguration iVoteConfiguration;
        private readonly ILogger<VotePostRepository> iLogger;
        private Container voteContainer;

        public VotePostRepository(
            IClientFactory iClientFactory,
            ILogger<VotePostRepository> iLogger,
            IVoteConfiguration iVoteConfiguration)
        {
            this.iClientFactory = iClientFactory;
            this.iVoteConfiguration = iVoteConfiguration;
            this.iLogger = iLogger;
            this.voteContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.iVoteConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosVoteContainer);
        }

        public async Task<List<VotePost>> GetVotePostAsync(List<Guid> listPost, string userId)
        {
            var votes = new List<VotePost>();

            try
            {
                var queryString = $"SELECT * FROM p WHERE p.userId = @UserId AND ARRAY_CONTAINS(@ListPost, p.postId) ORDER BY p.creationDate DESC";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                queryDef.WithParameter("@ListPost", listPost.ToArray());
                var query = this.voteContainer.GetItemQueryIterator<VotePost>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    votes.AddRange(response.ToList());
                }

            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }

            return votes;
        }

        public async Task<List<VotePost>> GetVotesByUserId(string userId)
        {
            var votes = new List<VotePost>();
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.type='vote' AND p.userId = @UserId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.voteContainer.GetItemQueryIterator<VotePost>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    votes.AddRange(response.ToList());
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }

            return votes;
        }
    }
}
