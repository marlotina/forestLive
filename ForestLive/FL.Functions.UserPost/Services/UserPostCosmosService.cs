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
        private Container usersFollowContainer;

        public UserPostCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.usersContainer = dbClient.GetContainer(databaseName, "post");
            this.usersFollowContainer = dbClient.GetContainer(databaseName, "userfollow");
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

        public async Task AddVoteAsync(VotePostBaseDto vote)
        {
            try
            {
                var obj = new dynamic[] { vote.PostId };
                await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseVoteCount", new PartitionKey(vote.AuthorPostId), obj);
            }
            catch (Exception ex)
            {

            }
            
        }

        public async Task DeleteVoteAsync(VotePostBaseDto vote)
        {

            var obj = new dynamic[] { vote.PostId };
            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCount", new PartitionKey(vote.AuthorPostId), obj);
        }

        public async Task DeleteCommentAsync(CommentBaseDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseCommentCount", new PartitionKey(comment.AuthorPostId), obj);
        }

        public async Task AddCommentAsync(CommentBaseDto comment)
        {
            try
            {

                var obj = new dynamic[] { comment.PostId.ToString() };
                await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseCommentCount", new PartitionKey(comment.AuthorPostId), obj);
            }
            catch (Exception ex)
            {

            }
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

        public async Task AddFollowerAsync(FollowerUser follower)
        {
            try
            {
                var obj = new dynamic[] { follower.UserId };
                await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseFollowerCount", new PartitionKey(follower.UserId), obj);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeleteFollowerAsync(FollowerUser follower)
        {
            try
            {
                var obj = new dynamic[] { follower.UserId };
                await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseFollowerCount", new PartitionKey(follower.UserId), obj);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
