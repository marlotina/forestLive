using FL.Functions.UserPost.Dto;
using FL.Functions.UserPost.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public class UserPostCosmosService : IUserPostCosmosService
    {
        private Container usersContainer;

        public UserPostCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.usersContainer = dbClient.GetContainer(databaseName, "users");
        }

        public async Task CreatePostInPendingAsync(Model.BirdPost post)
        {
            try
            {
                await usersContainer.CreateItemAsync(post, new PartitionKey(post.UserId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeletePostInPendingAsync(Model.BirdPost post)
        {
            try
            {
                await this.usersContainer.DeleteItemAsync<Model.BirdPost>(post.Id.ToString(), new PartitionKey(post.UserId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task AddVoteAsync(VotePostDto vote)
        {
            var obj = new dynamic[] { vote.PostId };
            var result = await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<VotePostDto>("createVote", new PartitionKey(vote.UserId.ToString()), obj);
        }

        public async Task DeleteVoteAsync(VotePostDto vote)
        {

            var obj = new dynamic[] { vote.PostId };
            var result = await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<VotePostDto>("deleteVote", new PartitionKey(vote.UserId.ToString()), obj);
        }

        public async Task DeleteCommentAsync(BirdCommentDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<BirdCommentDto>("deleteComment", new PartitionKey(comment.UserId.ToString()), obj);
        }

        public async Task AddCommentAsync(BirdCommentDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<VotePostDto>("addComment", new PartitionKey(comment.UserId.ToString()), obj);
        }
    }
}
