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
    public class UserVotesRepository : IUserVotesRepository
    {
        private readonly IUserPostConfiguration userPostConfiguration;

        public UserVotesRepository(
            IUserPostConfiguration userPostConfiguration)
        {
            this.userPostConfiguration = userPostConfiguration;
        }

        public async Task<List<Guid>> GetUserVoteByPosts(List<Guid> listPosts, string userId)
        {
            var client = new RestClient(this.userPostConfiguration.VoteApiDomain);
            var restRequest = new RestRequest(this.userPostConfiguration.VoteUrlService, Method.GET);
            
            var requestVoteUser = new VotePostRequest() {
                ListPosts = listPosts,
                UserId = userId
            };
            restRequest.AddJsonBody(requestVoteUser);

            var response = await client.ExecuteAsync<IEnumerable<Guid>>(restRequest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<Guid>>(response.Content);
            }

            return null;
        }
    }
}
