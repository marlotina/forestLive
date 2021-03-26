using FL.WebAPI.Core.Birds.Configuration.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Repository;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Infrastructure.Repositories
{
    public class UserVotesRepository : IUserVotesRepository
    {
        private readonly IBirdsConfiguration birdsConfiguration;

        public UserVotesRepository(
            IBirdsConfiguration birdsConfiguration)
        {
            this.birdsConfiguration = birdsConfiguration;
        }

        public async Task<IEnumerable<VotePostResponse>> GetUserVoteByPosts(IEnumerable<Guid> listPosts, string userId)
        {
            var client = new RestClient(this.birdsConfiguration.VoteApiDomain);
            var restRequest = new RestRequest(this.birdsConfiguration.VoteUrlService, Method.POST);
            
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
