using FL.CosmosDb.Standard.Contracts;
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
        private IClientFactory clientFactory;
        private IVoteConfiguration voteConfiguration;
        private Container commentVoteContainer;

        public VoteCommentRepository(IClientFactory clientFactory,
            IVoteConfiguration voteConfiguration)
        {
            this.clientFactory = clientFactory;
            this.voteConfiguration = voteConfiguration;
            this.commentVoteContainer = InitialCLient();
        }

        private Container InitialCLient()
        {
            var config = this.voteConfiguration.CosmosConfiguration;
            var dbClient = this.clientFactory.InitializeCosmosBlogClientInstanceAsync(config.CosmosDatabaseId);
            return dbClient.GetContainer(config.CosmosDatabaseId, config.CosmosCommentVoteContainer);
        }

        public async Task<List<VoteInfoDto>> GetVoteCommentsAsync(List<Guid> listComment, string userId)
        {
            var votes = new List<VoteInfoDto>();

            try
            {
                var queryString = $"SELECT p.commentId, p.id FROM p WHERE p.userId = @UserId AND ARRAY_CONTAINS(@ListComment, p.commentId) ORDER BY p.creationDate DESC";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                queryDef.WithParameter("@ListComment", listComment.ToArray());
                var query = this.commentVoteContainer.GetItemQueryIterator<VoteInfoDto>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    votes.AddRange(response.ToList());
                }

            }
            catch (Exception ex)
            {
            }

            return votes;
        }

        public async Task<List<VotePost>> GetVotesByUserId(string userId)
        {
            var votes = new List<VotePost>();
            try
            {
                var queryString = $"SELECT * FROM p WHERE p.type='votePost' AND p.userId = @UserId";

                var queryDef = new QueryDefinition(queryString);
                queryDef.WithParameter("@UserId", userId);
                var query = this.commentVoteContainer.GetItemQueryIterator<VotePost>(queryDef);

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    var ru = response.RequestCharge;
                    votes.AddRange(response.ToList());
                }
            }
            catch (Exception ex)
            {
            }

            return votes;
        }
    }
}
