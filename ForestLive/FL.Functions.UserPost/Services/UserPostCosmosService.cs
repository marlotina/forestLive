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

        public async Task AddVoteCountAsync(VotePostDto vote)
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

        public async Task DeleteVoteCountAsync(VotePostDto vote)
        {

            var obj = new dynamic[] { vote.PostId };
            await this.usersPostContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCount", new PartitionKey(vote.AuthorPostId), obj);
        }

        public async Task AddCommentCountAsync(CommentDto comment)
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

        public async Task DeleteCommentCountAsync(CommentDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.usersPostContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseCommentCount", new PartitionKey(comment.AuthorPostId), obj);
        }
    }
}
