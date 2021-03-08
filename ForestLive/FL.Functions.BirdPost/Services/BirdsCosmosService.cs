using FL.Functions.BirdPost.Dto;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public class BirdsCosmosDbService : IBirdsCosmosService
    {
        private Container birdsContainer;

        public BirdsCosmosDbService(CosmosClient dbClient, string databaseName)
        {
            this.birdsContainer = dbClient.GetContainer(databaseName, "birds");
        }

        public async Task CreatePostAsync(Model.BirdPost post)
        {
            await this.birdsContainer.CreateItemAsync(post, new PartitionKey(post.SpecieId.ToString()));
        }

        public async Task DeletePostAsync(Model.BirdPost post)
        {
            await this.birdsContainer.DeleteItemAsync<Model.BirdPost>(post.Id.ToString(), new PartitionKey(post.SpecieId.ToString()));
        }

        public async Task AddCommentAsync(BirdCommentDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string>("addComment", new PartitionKey(comment.SpecieId.ToString()), obj);
        }

        public async Task DeleteCommentAsync(BirdCommentDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string> ("deleteComment", new PartitionKey(comment.SpecieId.ToString()), obj);
        }

        public async Task AddVoteAsync(VotePostDto vote)
        {
            var obj = new dynamic[] { vote.PostId };
            await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string>("addVote", new PartitionKey(vote.SpecieId.ToString()), obj);
        }

        public async Task DeleteVoteAsync(VotePostDto vote)
        {

            var obj = new dynamic[] { vote.PostId };
            await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string>("deleteVote", new PartitionKey(vote.SpecieId.ToString()), obj);
        }
    }
}
