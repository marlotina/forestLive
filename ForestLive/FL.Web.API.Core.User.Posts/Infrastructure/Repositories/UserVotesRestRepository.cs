using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.Web.API.Core.User.Posts.Domain.Repositories;
using FL.WebAPI.Core.User.Posts.Configuration.Contracts;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Infrastructure.Repositories
{
    public class UserVotesRestRepository : IUserVotesRestRepository
    {
        private readonly IUserPostConfiguration iUserPostConfiguration;
        private readonly ILogger<UserVotesRestRepository> iLogger;

        public UserVotesRestRepository(
            ILogger<UserVotesRestRepository> iLogger,
            IUserPostConfiguration iUserPostConfiguration)
        {
            this.iLogger = iLogger;
            this.iUserPostConfiguration = iUserPostConfiguration;
        }

        public async Task<IEnumerable<VotePostResponse>> GetUserVoteByPosts(IEnumerable<Guid> listPosts, string userId)
        {
            try
            {
                var client = new RestClient(this.iUserPostConfiguration.VoteApiDomain);
                var restRequest = new RestRequest(this.iUserPostConfiguration.VoteUrlService, Method.POST);

                var requestVoteUser = new VotePostRequest()
                {
                    ListPosts = listPosts,
                    UserId = userId
                };

                restRequest.AddJsonBody(requestVoteUser);

                var response = await client.ExecuteAsync<IEnumerable<VotePostResponse>>(restRequest);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<VotePostResponse>>(response.Content);
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }

            return null;
        }
    }
}
