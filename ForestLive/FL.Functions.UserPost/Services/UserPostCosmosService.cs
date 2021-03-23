using FL.Functions.UserPost.Dto;
using FL.Functions.UserPost.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
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
                await usersContainer.CreateItemAsync(post, new PartitionKey(post.UserId));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeletePostInPendingAsync(Model.BirdPost post)
        {
            try
            {
                await this.usersContainer.DeleteItemAsync<Model.BirdPost>(post.Id.ToString(), new PartitionKey(post.UserId));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task AddVoteAsync(VotePost vote)
        {
            try
            {
                var obj = new dynamic[] { vote.PostId };
                await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("addVote", new PartitionKey(vote.UserId), obj);
            }
            catch (Exception ex)
            {

            }
            
        }

        public async Task DeleteVoteAsync(VotePost vote)
        {

            var obj = new dynamic[] { vote.PostId };
            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("deleteVote", new PartitionKey(vote.UserId), obj);
        }

        public async Task DeleteCommentAsync(BirdCommentDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("deleteComment", new PartitionKey(comment.UserId), obj);
        }

        public async Task AddCommentAsync(BirdCommentDto comment)
        {
            var obj = new dynamic[] { comment.PostId };
            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("addComment", new PartitionKey(comment.UserId), obj);
        }

        public async Task AddLabelAsync(List<UserLabel> labels)
        {
            try
            {
                foreach (var label in labels) 
                {
                    ItemResponse<UserLabel> response = null;
                    try
                    {
                        response = await this.usersContainer.ReadItemAsync<UserLabel>(label.Id, new PartitionKey(label.UserId));
                    }
                    catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) { }

                    if (response != null && !string.IsNullOrEmpty(response.Resource.Id))
                    {
                        var obj = new dynamic[] { response.Resource.Id };
                        await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("updateLabel", new PartitionKey(response.Resource.UserId), obj);
                    }
                    else { 
                        await this.usersContainer.CreateItemAsync(label, new PartitionKey(label.UserId));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
