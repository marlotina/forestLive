using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Domain.Dto;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class UserFollowRepository : IUserFollowRepository
    {
        private IUserConfiguration iUserConfiguration;

        public UserFollowRepository(
            IUserConfiguration iUserConfiguration)
        {
            this.iUserConfiguration = iUserConfiguration;
        }
        public async Task<bool> GetFollow(string userId, string followUserId)
        {
            var client = new RestClient(this.iUserConfiguration.UserInteractionApiDomain);
            var restRequest = new RestRequest(this.iUserConfiguration.FollowUrlService, Method.GET);


            restRequest.AddQueryParameter("userId", userId);
            restRequest.AddQueryParameter("followUserId", followUserId);

            var response = await client.ExecuteAsync<FollowUserResponse>(restRequest);
            
            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            if (string.IsNullOrEmpty(JsonConvert.DeserializeObject<FollowUserResponse>(response.Content).FollowerId)) {
                    return false;
            }
            
            return true;
        }
    }
}
