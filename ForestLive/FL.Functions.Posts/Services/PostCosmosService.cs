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
            this.postContainer = dbClient.GetContainer(databaseName, "specie");
        }

        public async Task CreatePostAsync(Model.BirdPost post)
        {
            try
            {
                await this.postContainer.CreateItemAsync(post, new PartitionKey(post.SpecieId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdatePostAsync(BirdPost post)
        {
            await this.postContainer.UpsertItemAsync<BirdPost>(post, new PartitionKey(post.PostId.ToString()));
        }

        public async Task DeletePostAsync(Model.BirdPost post)
        {
            try
            {
                await this.postContainer.DeleteItemAsync<Model.BirdPost>(post.Id.ToString(), new PartitionKey(post.PostId.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task AddCommentPostAsync(CommentBaseDto comment)
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

        public async Task AddVotePostAsync(VotePostBaseDto vote)
        {
            try
            {
                var obj = new dynamic[] { vote.PostId };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseVoteCount", new PartitionKey(vote.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task DeleteCommentPostAsync(CommentBaseDto comment)
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

        public async Task DeleteVotePostAsync(VotePostBaseDto vote)
        {
            try
            {
                var obj = new dynamic[] { vote.PostId };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseVoteCount", new PartitionKey(vote.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
