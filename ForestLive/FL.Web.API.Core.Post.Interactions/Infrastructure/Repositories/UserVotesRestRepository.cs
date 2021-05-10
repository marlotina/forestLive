﻿using FL.Web.API.Core.Post.Interactions.Configuration.Contracts;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Repositories;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Infrastructure.Repositories
{
    public class UserVotesRestRepository : IUserVotesRestRepository
    {
        private IPostConfiguration iVoteConfiguration;

        public UserVotesRestRepository(
            IPostConfiguration iVoteConfiguration)
        {
            this.iVoteConfiguration = iVoteConfiguration;
        }

        public async Task<IEnumerable<VotePostResponse>> GetUserVoteByComments(IEnumerable<Guid> listComments, string userId)
        {
            if (string.IsNullOrEmpty(userId) || listComments == null || !listComments.Any()) {
                return null;
            }

            var client = new RestClient(this.iVoteConfiguration.VoteApiDomain);
            var restRequest = new RestRequest(this.iVoteConfiguration.VoteUrlService, Method.POST);
            
            var requestVoteUser = new VotePostRequest() {
                ListComments = listComments,
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