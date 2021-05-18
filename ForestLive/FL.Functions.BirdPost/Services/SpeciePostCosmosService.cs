using FL.Functions.BirdPost.Dto;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public class SpeciePostCosmosService : ISpeciePostCosmosService
    {
        private Container specieContainer;

        public SpeciePostCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.specieContainer = dbClient.GetContainer(databaseName, "specie");
        }

        public async Task AddCommentCountAsync(CommentBaseDto comment)
        {
            try
            {
                var obj = new dynamic[] { comment.PostId };
                await this.specieContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseCommentCount", new PartitionKey(comment.SpecieId.ToString()), obj);
            }
            catch (Exception ex) 
            { 
            
            }
        }

        public async Task DeleteCommentCountAsync(CommentBaseDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.specieContainer.Scripts.ExecuteStoredProcedureAsync<string> ("decreaseCommentCount", new PartitionKey(comment.SpecieId.ToString()), obj);
        }

        public async Task AddVotePostCountAsync(VotePostBaseDto vote)
        {
            var obj = new dynamic[] { vote.PostId };
            await this.specieContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseVoteCount", new PartitionKey(vote.SpecieId.ToString()), obj);
        }

        public async Task DeleteVotePostCountAsync(VotePostBaseDto vote)
        {
            var obj = new dynamic[] { vote.PostId };
            await this.specieContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCount", new PartitionKey(vote.SpecieId.ToString()), obj);
        }

        public async Task CreatePostAsync(Dto.BirdPost post)
        {
            try
            {
                await this.specieContainer.UpsertItemAsync(post, new PartitionKey(post.PostId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
