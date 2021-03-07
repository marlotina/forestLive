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
            this.postContainer = dbClient.GetContainer(databaseName, "posts");
        }

        public async Task AddCommentPostAsync(BirdComment comment)
        {
            try
            {
                var obj = new dynamic[] { comment.PostId, comment };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<BirdComment>("createComment", new PartitionKey(comment.PostId.ToString()), obj);
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
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<BirdComment>("createVote", new PartitionKey(vote.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task DeleteCommentPostAsync(BirdComment comment)
        {
            try
            {
                var obj = new dynamic[] { comment.PostId, comment };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<string>("deleteComment", new PartitionKey(comment.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task DeleteVotePostAsync(VotePostDto vote)
        {
            try
            {
                var obj = new dynamic[] { vote.PostId, vote };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<string>("deleteComment", new PartitionKey(vote.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
