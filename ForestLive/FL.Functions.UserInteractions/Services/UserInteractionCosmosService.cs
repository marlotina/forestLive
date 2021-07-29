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
        private readonly Container usersCommentContainer;
        private readonly Container usersVoteContainer;
        private readonly Container usersContainer;

        public UserInteractionCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.usersCommentContainer = dbClient.GetContainer(databaseName, "comment");
            this.usersVoteContainer = dbClient.GetContainer(databaseName, "vote");
            this.usersContainer = dbClient.GetContainer(databaseName, "user");
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
                var obj = new dynamic[] { follower.UserId };
                
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
                var obj = new dynamic[] { follower.UserId };
                await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("decreaseFollowerCount", new PartitionKey(follower.UserId), obj);
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