using FL.Functions.BirdPost.Dto;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public class BirdsCosmosService : IBirdsCosmosService
    {
        private Container birdsContainer;

        public BirdsCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.birdsContainer = dbClient.GetContainer(databaseName, "specie");
        }

        public async Task AddCommentAsync(CommentBaseDto comment)
        {
            try
            {
                var obj = new dynamic[] { comment.PostId };
                await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseCommentCount", new PartitionKey(comment.SpecieId.ToString()), obj);
            }
            catch (Exception ex) 
            { 
            
            }
        }

        public async Task DeleteCommentAsync(CommentBaseDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string> ("decreaseCommentCount", new PartitionKey(comment.SpecieId.ToString()), obj);
        }

        public async Task AddVotePostAsync(VotePostBaseDto vote)
        {
            var obj = new dynamic[] { vote.PostId };
            await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseVoteCount", new PartitionKey(vote.SpecieId.ToString()), obj);
        }

        public async Task DeleteVotePostAsync(VotePostBaseDto vote)
        {
            var obj = new dynamic[] { vote.PostId };
            await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCount", new PartitionKey(vote.SpecieId.ToString()), obj);
        }

        public async Task CreatePostAsync(Dto.BirdPost post)
        {
            try
            {
                await this.birdsContainer.UpsertItemAsync(post, new PartitionKey(post.PostId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
