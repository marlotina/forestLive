using FL.Functions.Posts.Dto;
using FL.Functions.Posts.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.Posts.Services
{
    public class PostCosmosService : IPostCosmosService
    {
        private Container postContainer;

        public PostCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.postContainer = dbClient.GetContainer(databaseName, "post");
        }

        public async Task AddCommentPostAsync(BirdCommentDto comment)
        {
            try
            {
                var obj = new dynamic[] { comment.PostId };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<BirdComment>("increaseCommentCount", new PartitionKey(comment.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task AddVotePostAsync(VotePostDto vote)
        {
            try
            {
                var obj = new dynamic[] { vote.PostId, vote };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseVoteCount", new PartitionKey(vote.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task DeleteCommentPostAsync(BirdCommentDto comment)
        {
            try
            {
                var obj = new dynamic[] { comment.PostId };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseCommentCount", new PartitionKey(comment.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task DeleteVotePostAsync(VotePostDto vote)
        {
            try
            {
                var obj = new dynamic[] { vote.PostId, vote.Id };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCount", new PartitionKey(vote.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
