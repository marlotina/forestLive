using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Bird.Pending.Configuration.Contracts;
using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using FL.Web.API.Core.Bird.Pending.Domain.Repository;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Infrastructure.Repositories
{
    public class UserVotesRestRepository : IUserVotesRestRepository
    {
        private readonly IBirdPendingConfiguration iBirdsConfiguration;
        private readonly ILogger<UserVotesRestRepository> iLogger;
        public UserVotesRestRepository(
            ILogger<UserVotesRestRepository> iLogger,
            IBirdPendingConfiguration iBirdsConfiguration)
        {
            this.iBirdsConfiguration = iBirdsConfiguration;
            this.iLogger = iLogger;
        }

        public async Task<IEnumerable<VotePostResponse>> GetUserVoteByPosts(IEnumerable<Guid> listPosts, string userId)
        {
            try
            {
                var client = new RestClient(this.iBirdsConfiguration.VoteApiDomain);
                var restRequest = new RestRequest(this.iBirdsConfiguration.VoteUrlService, Method.POST);

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
