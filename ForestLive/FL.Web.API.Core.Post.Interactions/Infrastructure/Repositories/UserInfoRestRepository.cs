using FL.Web.API.Core.Post.Interactions.Configuration.Contracts;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Repositories;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Infrastructure.Repositories
{
    public class UserInfoRestRepository : IUserInfoRestRepository
    {
        private readonly IPostConfiguration iPostConfiguration;

        public UserInfoRestRepository(
            IPostConfiguration iPostConfiguration)
        {
            this.iPostConfiguration = iPostConfiguration;
        }

        public async Task<IEnumerable<InfoUserResponse>> GetUsers()
        {
            var client = new RestClient(this.iPostConfiguration.UserApiDomain);
            var restRequest = new RestRequest(this.iPostConfiguration.UserUrlService, Method.GET);
            
            var response = await client.ExecuteAsync<IEnumerable<InfoUserResponse>>(restRequest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<InfoUserResponse>>(response.Content);
            }

            return null;
        }
    }
}
