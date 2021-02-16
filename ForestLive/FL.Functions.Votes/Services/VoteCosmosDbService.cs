using FL.Functions.Votes.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.Votes.Services
{
    public class VoteCosmosDbService : IVoteCosmosDbService
    {
        
        private Container voteContainer;

        public VoteCosmosDbService(CosmosClient dbClient, string databaseName)
        {
            voteContainer = dbClient.GetContainer(databaseName, "votes");
        }
        
        public async Task CreateVoteInUserAsync(VotePostDto vote)
        {
            try
            {
                await voteContainer.CreateItemAsync(vote, new PartitionKey(vote.UserId));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
