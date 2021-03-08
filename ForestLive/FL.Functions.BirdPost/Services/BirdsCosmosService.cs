using FL.Functions.BirdPost.Model;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public class BirdsCosmosService : IBirdsCosmosService
    {
        private Container birdsContainer;

        public BirdsCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.birdsContainer = dbClient.GetContainer(databaseName, "birds");
        }

        public async Task AddVoteAsync(VotePostDto vote)
        {
            var obj = new dynamic[] { vote.PostId };
            var result = await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<VotePostDto>("createVote", new PartitionKey(vote.SpecieId.ToString()), obj);
        }

        public async Task CreatePostAsync(BirdPostDto post)
        {
            await this.birdsContainer.CreateItemAsync(post, new PartitionKey(post.SpecieId.ToString()));
        }

        public async Task DeletePostAsync(BirdPostDto deletePostRequest)
        {
            await this.birdsContainer.DeleteItemAsync<BirdPostDto>(deletePostRequest.Id.ToString(), new PartitionKey(deletePostRequest.SpecieId.ToString()));
        }

        public async Task DeleteVoteAsync(VotePostDto vote)
        {

            var obj = new dynamic[] { vote.PostId };
            var result = await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<VotePostDto>("deleteVote", new PartitionKey(vote.SpecieId.ToString()), obj);
        }
    }
}
