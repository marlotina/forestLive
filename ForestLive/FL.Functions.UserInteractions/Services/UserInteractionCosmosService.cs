using Fl.Functions.UserInteractions.Dto;
using Fl.Functions.UserInteractions.Model;
using FL.Functions.UserInteractions.Dto;
using FL.Functions.UserInteractions.Model;
using FL.Functions.UserInteractions.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddLabelAsync(IEnumerable<UserLabel> labels)
        {
            try
            {
                if (labels != null && labels.Any())
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
                            await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("updateLabelCount", new PartitionKey(response.Resource.UserId), obj);
                        }
                        else
                        {
                            await this.usersContainer.CreateItemAsync(label, new PartitionKey(label.UserId));
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public async Task RemovePostLabelAsync(IEnumerable<RemoveLabelDto> removeLabels)
        {
            try
            {
                if (removeLabels != null && removeLabels.Any())
                {
                    foreach (var label in removeLabels)
                    {
                        var obj = new dynamic[] { label.Label };
                        await this.usersContainer.Scripts.ExecuteStoredProcedureAsync<string>("deletePostLabelCount", new PartitionKey(label.UserId), obj);
                    }
                }
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