using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Domain.Dto;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class UserFollowRepository : IUserFollowRepository
    {
        private readonly IUserConfiguration iUserConfiguration;
        private readonly ILogger<UserFollowRepository> iLogger;
        public UserFollowRepository(
            ILogger<UserFollowRepository> iLogger,
            IUserConfiguration iUserConfiguration)
        {
            this.iUserConfiguration = iUserConfiguration;
            this.iLogger = iLogger;
        }
        public async Task<bool> GetFollow(string userId, string followUserId)
        {
            try
            {
                var client = new RestClient(this.iUserConfiguration.UserInteractionApiDomain);
                var restRequest = new RestRequest(this.iUserConfiguration.FollowUrlService, Method.GET);


                restRequest.AddQueryParameter("userId", userId);
                restRequest.AddQueryParameter("followUserId", $"{userId}_{followUserId}");

                var response = await client.ExecuteAsync<FollowUserResponse>(restRequest);

                if (response.StatusCode != HttpStatusCode.OK)
                    return false;

                if (string.IsNullOrEmpty(JsonConvert.DeserializeObject<FollowUserResponse>(response.Content).FollowerId))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }
    }
}
