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

        public async Task AddCommentAsync(CommentPostDto comment)
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

        public async Task DeleteCommentAsync(CommentPostDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string> ("decreaseCommentCount", new PartitionKey(comment.SpecieId.ToString()), obj);
        }

        public async Task AddVotePostAsync(VotePostDto vote)
        {
            var obj = new dynamic[] { vote.PostId };
            await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseVoteCount", new PartitionKey(vote.SpecieId.ToString()), obj);
        }

        public async Task DeleteVotePostAsync(VotePostDto vote)
        {
            var obj = new dynamic[] { vote.PostId };
            await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCount", new PartitionKey(vote.SpecieId.ToString()), obj);
        }

        public async Task CreatePostAsync(Dto.BirdPost post)
        {
            try
            {
                await this.birdsContainer.CreateItemAsync(post, new PartitionKey(post.SpecieId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdateSpecieAsync(Dto.BirdPost post)
        {
            try
            {
                var obj = new dynamic[] { post };
                var response = await this.birdsContainer.Scripts.ExecuteStoredProcedureAsync<string>("updateSpecie", new PartitionKey(post.SpecieId.ToString()), obj);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
