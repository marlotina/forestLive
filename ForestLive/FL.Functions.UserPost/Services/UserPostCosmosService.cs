using FL.Functions.UserPost.Dto;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public class UserPostCosmosService : IUserPostCosmosService
    {
        private Container usersPostContainer;

        public UserPostCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.usersPostContainer = dbClient.GetContainer(databaseName, "user");
        }

        public async Task CreatePostAsync(Model.Post post)
        {
            try
            {
                await usersPostContainer.CreateItemAsync(post, new PartitionKey(post.UserId));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdatePostAsync(Model.Post post)
        {
            try
            {
                await this.usersPostContainer.UpsertItemAsync(post, new PartitionKey(post.UserId));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeletePostAsync(Model.Post post)
        {
            try
            {
                await this.usersPostContainer.DeleteItemAsync<Model.Post>(post.Id.ToString(), new PartitionKey(post.UserId));
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
                await this.usersPostContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseVoteCount", new PartitionKey(vote.AuthorPostId), obj);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeleteVoteAsync(VotePostDto vote)
        {

            var obj = new dynamic[] { vote.PostId };
            await this.usersPostContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCount", new PartitionKey(vote.AuthorPostId), obj);
        }

        public async Task AddCommentAsync(CommentDto comment)
        {
            try
            {

                var obj = new dynamic[] { comment.PostId };
                await this.usersPostContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseCommentCount", new PartitionKey(comment.AuthorPostId), obj);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeleteCommentAsync(CommentDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.usersPostContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseCommentCount", new PartitionKey(comment.AuthorPostId), obj);
        }
    }
}
