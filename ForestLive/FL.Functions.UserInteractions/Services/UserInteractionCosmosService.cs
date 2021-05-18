using FL.Functions.UserInteractions.Dto;
using FL.Functions.UserInteractions.Model;
using FL.Functions.UserInteractions.Services;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public class UserInteractionCosmosService : IUserInterationCosmosService
    {
        private Container usersCommentContainer;
        private Container usersVoteContainer;
        private Container usersCommentVoteContainer;

        public UserInteractionCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.usersCommentContainer = dbClient.GetContainer(databaseName, "comment");
            this.usersVoteContainer = dbClient.GetContainer(databaseName, "vote");
            this.usersCommentVoteContainer = dbClient.GetContainer(databaseName, "commentvote");
        }

        public async Task AddCommentPostAsync(CommentDto comment)
        {
            try
            {
                await this.usersCommentContainer.CreateItemAsync<CommentDto>(comment, new PartitionKey(comment.UserId));
            }
            catch (Exception ex)
            {
            }
        }

        public async Task AddCommentVotePostAsync(VoteCommentPostDto vote)
        {
            try
            {
                await this.usersCommentVoteContainer.CreateItemAsync<VoteCommentPostDto>(vote, new PartitionKey(vote.UserId));
            }
            catch (Exception ex)
            {
            }
        }

        public async Task AddVotePostAsync(VotePostDto vote)
        {
            try
            {
                var request = this.ConvertVote(vote);
                var obj = new dynamic[] { request };
                await this.usersVoteContainer.Scripts.ExecuteStoredProcedureAsync<VotePost>("addVote", new PartitionKey(vote.UserId), obj);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task DeleteCommentPostAsync(CommentBaseDto comment)
        {
            try
            {
                await this.usersCommentContainer.DeleteItemAsync<CommentDto>(comment.Id.ToString(), new PartitionKey(comment.UserId));
            }
            catch (Exception ex)
            {
            }
        }

        public async Task DeleteCommentVotePostAsync(VoteCommentPostDto vote)
        {
            try
            {
                await this.usersCommentVoteContainer.DeleteItemAsync<VoteCommentPostDto>(vote.Id.ToString(), new PartitionKey(vote.UserId));
            }
            catch (Exception ex)
            {
            }
        }

        public async Task DeleteVotePostAsync(VotePostBaseDto vote)
        {
            try
            {
                var obj = new dynamic[] { vote.Id };
                var result = await this.usersVoteContainer.Scripts.ExecuteStoredProcedureAsync<string>("deleteVote", new PartitionKey(vote.UserId), obj);
            }
            catch (Exception ex)
            {
            }
        }

        private VotePost ConvertVote(VotePostDto source) 
        {
            return new VotePost() {
                Id = source.Id,
                Type = source.Type,
                PostId = source.PostId,
                AuthorPostId = source.AuthorPostId,
                UserId = source.UserId,
                TitlePost = source.TitlePost,
                CreationDate = source.CreationDate,
                SpecieId = source.SpecieId
            };
        }
    }
}