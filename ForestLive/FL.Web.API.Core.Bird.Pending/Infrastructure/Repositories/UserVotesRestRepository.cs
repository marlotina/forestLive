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

        public UserVotesRestRepository(
            IBirdPendingConfiguration iBirdsConfiguration)
        {
            this.iBirdsConfiguration = iBirdsConfiguration;
        }

        public async Task<IEnumerable<VotePostResponse>> GetUserVoteByPosts(IEnumerable<Guid> listPosts, string userId)
        {
            var client = new RestClient(this.iBirdsConfiguration.VoteApiDomain);
            var restRequest = new RestRequest(this.iBirdsConfiguration.VoteUrlService, Method.POST);
            
            var requestVoteUser = new VotePostRequest() {
                ListPosts = listPosts,
                UserId = userId
            };

            restRequest.AddJsonBody(requestVoteUser);

            var response = await client.ExecuteAsync<IEnumerable<VotePostResponse>>(restRequest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<VotePostResponse>>(response.Content);
            }

            return null;
        }
    }
}
