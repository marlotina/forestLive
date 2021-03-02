using FL.Functions.UserPost.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public class CommentPostCosmosDbService : ICommentPostCosmosDbService
    {
        private Container commentContainer;

        public CommentPostCosmosDbService(CosmosClient dbClient, string databaseName)
        {
            this.commentContainer = dbClient.GetContainer(databaseName, "comments");
        }

        public async Task CreatePostInPendingAsync(CommentPostDto comment)
        {
            try
            {
                await this.commentContainer.CreateItemAsync(comment, new PartitionKey(comment.UserId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeletePostInPendingAsync(CommentPostDto comment)
        {
            try
            {
                await this.commentContainer.DeleteItemAsync<CommentPostDto>(comment.Id.ToString(), new PartitionKey(comment.UserId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
