using FL.Functions.BirdPost.Dto;
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

        public async Task CreatePostAsync(Model.Post post)
        {
            await this.birdsContainer.CreateItemAsync(post, new PartitionKey(post.SpecieId.ToString()));
        }

        public async Task DeletePostAsync(Model.Post post)
        {
            await this.birdsContainer.DeleteItemAsync<Model.Post>(post.Id.ToString(), new PartitionKey(post.SpecieId.ToString()));
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
    }
}
