using FL.CosmosDb.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Configuration.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Dto;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Infrastructure.Repositories
{
    public class VoteCommentRepository : IVoteCommentRepository
    {
        private readonly IClientFactory iClientFactory;
        private readonly IUserInteractionsConfiguration iVoteConfiguration;
        private readonly ILogger<VoteCommentRepository> iLogger;
        private Container commentVoteContainer;

        public VoteCommentRepository(
            IClientFactory clientFactory,
            ILogger<VoteCommentRepository> iLogger,
            IUserInteractionsConfiguration voteConfiguration)
        {
            this.iClientFactory = clientFactory;
            this.iLogger = iLogger;
            this.iVoteConfiguration = voteConfiguration;
            this.commentVoteContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.iVoteConfiguration.CosmosConfiguration;
            var dbClient = this.iClientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosCommentVoteContainer);
        }

        public async Task<List<VoteCommentPost>> GetVotesByUserId(string userId)
        {
            var votes = new List<VoteCommentPost>();
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.type='voteComment' AND p.userId = @UserId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.commentVoteContainer.GetItemQueryIterator<VoteCommentPost>(queryDef);

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
