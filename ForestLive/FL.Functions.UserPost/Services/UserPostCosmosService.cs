using FL.Functions.UserPost.Dto;
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
            this.usersContainer = dbClient.GetContainer(databaseName, "post");
        }

        public async Task CreatePostAsync(Model.BirdPost post)
        {
            try
            {
                await usersContainer.CreateItemAsync(post, new PartitionKey(post.UserId));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeletePostAsync(Model.BirdPost post)
        {
            try
            {
                await this.usersContainer.DeleteItemAsync<Model.BirdPost>(post.Id.ToString(), new PartitionKey(post.UserId));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task AddVoteAsync(VotePostDto vote)
        {
            try
            {
                var obj = new dynamic[] { vote.PostId };
                await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseVoteCount", new PartitionKey(vote.UserId), obj);
            }
            catch (Exception ex)
            {

            }
            
        }

        public async Task DeleteVoteAsync(VotePostDto vote)
        {

            var obj = new dynamic[] { vote.PostId };
            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCount", new PartitionKey(vote.UserId), obj);
        }

        public async Task DeleteCommentAsync(BirdCommentDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseCommentCount", new PartitionKey(comment.UserId), obj);
        }

        public async Task AddCommentAsync(BirdCommentDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseCommentCount", new PartitionKey(comment.UserId), obj);
        }

        public async Task UpdatePostAsync(Model.BirdPost post)
        {
            try
            {
                await this.usersContainer.UpsertItemAsync(post, new PartitionKey(post.UserId));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
