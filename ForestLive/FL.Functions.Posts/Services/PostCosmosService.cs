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
            this.postContainer = dbClient.GetContainer(databaseName, "posts");
        }

        public async Task AddCommentPostAsync(BirdCommentDto comment)
        {
            try
            {
                var document = new BirdComment()
                {
                    Id = comment.Id,
                    PostId = comment.PostId,
                    Type = comment.Type,
                    Text = comment.Text,
                    CreateDate = comment.CreateDate,
                    UserId = comment.UserId 
                };

                var obj = new dynamic[] { comment.PostId, document };
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
                var document = new VotePost()
                {
                    Id = vote.Id,
                    PostId = vote.PostId,
                    Type = vote.Type,
                    CreationDate = vote.CreationDate,
                    UserId = vote.UserId,
                    OwnerUserId = vote.OwnerUserId,
                    Title = vote.Title
                };

                var obj = new dynamic[] { vote.PostId, document };
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<BirdComment>("createVote", new PartitionKey(vote.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task DeleteCommentPostAsync(BirdCommentDto comment)
        {
            try
            {
                var obj = new dynamic[] { comment.PostId, comment.Id };
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
                var result = await this.postContainer.Scripts.ExecuteStoredProcedureAsync<VotePostFunction>("deleteVote", new PartitionKey(vote.PostId.ToString()), obj);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
