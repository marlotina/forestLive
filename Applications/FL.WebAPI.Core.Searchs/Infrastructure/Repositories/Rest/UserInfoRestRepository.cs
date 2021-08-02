using FL.WebAPI.Core.Searchs.Configuration.Contracts;
using FL.WebAPI.Core.Searchs.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Repository;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Infrastructure.Repositories
{
    public class UserInfoRestRepository : IUserInfoRestRepository
    {
        private readonly IBirdsConfiguration iBirdsConfiguration;

        public UserInfoRestRepository(
            IBirdsConfiguration iBirdsConfiguration)
        {
            this.iBirdsConfiguration = iBirdsConfiguration;
        }

        public async Task<IEnumerable<InfoUserResponse>> GetUsers()
        {
            var client = new RestClient(this.iBirdsConfiguration.UserApiDomain);
            var restRequest = new RestRequest(this.iBirdsConfiguration.UserUrlService, Method.GET);
            
            var response = await client.ExecuteAsync<IEnumerable<InfoUserResponse>>(restRequest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<InfoUserResponse>>(response.Content);
            }

            return null;
        }
    }
}
