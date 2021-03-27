﻿using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Dto;
using FL.WebAPI.Core.Items.Domain.Repositories;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class UserVotesRepository : IUserVotesRepository
    {
        private readonly IPostConfiguration iPostConfiguration;

        public UserVotesRepository(
            IPostConfiguration iPostConfiguration)
        {
            this.iPostConfiguration = iPostConfiguration;
        }

        public async Task<IEnumerable<VotePostResponse>> GetUserVoteByPosts(IEnumerable<Guid> listPosts, string userId)
        {
            var client = new RestClient(this.iPostConfiguration.VoteApiDomain);
            var restRequest = new RestRequest(this.iPostConfiguration.VoteUrlService, Method.POST);
            
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
