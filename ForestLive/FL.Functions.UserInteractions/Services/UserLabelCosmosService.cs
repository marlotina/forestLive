using FL.Functions.UserInteractions.Dto;
using FL.Functions.UserInteractions.Model;
using FL.Functions.UserInteractions.Services;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace FL.Functions.UserPost.Services
{
    public class UserLabelCosmosService : IUserLabelCosmosService
    {
        private Container usersCommentContainer;
        private Container usersVoteContainer;

        public UserLabelCosmosService(CosmosClient dbClient, string databaseName)
        {
            this.usersCommentContainer = dbClient.GetContainer(databaseName, "usercomment");
            this.usersVoteContainer = dbClient.GetContainer(databaseName, "uservote");
        }

        public async Task AddCommentPostAsync(BirdCommentDto comment)
        {
            try
            {
                var request = this.ConvertComment(comment);
                var obj = new dynamic[] { request };
                var result = await this.usersCommentContainer.Scripts.ExecuteStoredProcedureAsync<BirdComment>("addComment", new PartitionKey(comment.UserId), obj);
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

        public async Task DeleteCommentPostAsync(BirdCommentDto comment)
        {
            try
            {
                var obj = new dynamic[] { comment.Id };
                var result = await this.usersCommentContainer.Scripts.ExecuteStoredProcedureAsync<string>("deleteComment", new PartitionKey(comment.UserId), obj);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task DeleteVotePostAsync(VotePostDto vote)
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
            var response = default(VotePost);

            response = new VotePost() {
                Id = source.Id,
                Type = source.Type,
                PostId = source.PostId,
                AuthorPostUserId = source.AuthorPostUserId,
                UserId = source.UserId,
                TitlePost = source.TitlePost,
                CreationDate = source.CreationDate,
                SpecieId = source.SpecieId
            };

            return response;
        }

        private BirdComment ConvertComment(BirdCommentDto source)
        {
            var response = default(BirdComment);

            response = new BirdComment()
            {
                Id = source.Id,
                Type = source.Type,
                PostId = source.PostId,
                //AuthorPostUserId = source.Text,
                Text = source.Text,
                UserId = source.UserId,
                //TitlePost = source.TitlePost,
                CreationDate = source.CreationDate,
                SpecieId = source.SpecieId
            };

            return response;
        }
    }
}