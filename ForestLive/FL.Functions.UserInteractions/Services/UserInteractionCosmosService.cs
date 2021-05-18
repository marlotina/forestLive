using FL.Functions.UserInteractions.Dto;
using FL.Functions.UserInteractions.Model;
using FL.Functions.UserInteractions.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using System;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public class UserInteractionCosmosService : IUserInterationCosmosService
    {
        private Container usersCommentContainer;
        private Container usersVoteContainer;
        private Container usersCommentVoteContainer;
        private Container usersContainer;

        public UserInteractionCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.usersCommentContainer = dbClient.GetContainer(databaseName, "comment");
            this.usersVoteContainer = dbClient.GetContainer(databaseName, "vote");
            this.usersCommentVoteContainer = dbClient.GetContainer(databaseName, "commentvote");
            this.usersContainer = dbClient.GetContainer(databaseName, "user");
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

        public async Task AddFollowerAsync(UserFollowDto follower)
        {
            try
            {
                var followerRequest = new FollowerUser()
                {
                    Id = follower.Id,
                    CreationDate = follower.CreationDate,
                    Type = follower.Type,
                    UserId = follower.UserId,
                    FollowUserId = follower.FollowUserId
                };

                var obj = new dynamic[] { followerRequest, follower.SystemUserId.ToString() };
                //await this.usersFollowContainer.Scripts.ExecuteStoredProcedureAsync<string>("increaseFollowerCount", new PartitionKey(follower.UserId), obj);

                StoredProcedureExecuteResponse<string> sprocResponse2 =
                    await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>(
                    "increaseFollowerCount",
                    new PartitionKey(follower.UserId),
                    obj);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeleteFollowerAsync(UserFollowDto follower)
        {
            try
            {
                var obj = new dynamic[] { follower.Id, follower.SystemUserId.ToString() };
                await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseFollowerCount", new PartitionKey(follower.FollowUserId), obj);
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