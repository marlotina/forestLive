using FL.Functions.UserPost.Dto;
using FL.Functions.UserPost.Model;
using FL.Functions.UserPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.BirdPost
{
    public class UserFollow
    {
        private readonly IUserPostCosmosService iUserPostCosmosDbService;

        public UserFollow(IUserPostCosmosService iUserPostCosmosDbService)
        {
            this.iUserPostCosmosDbService = iUserPostCosmosDbService;
        }

        [FunctionName("UserFollow")]
        public void Run([ServiceBusTrigger(
            "follower",
            "followerUserSubscription",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var follower = JsonConvert.DeserializeObject<UserFollowDto>(Encoding.UTF8.GetString(message.Body));
                
                if (message.Label == "createFollow")
                {
                    this.iUserPostCosmosDbService.AddFollowerAsync(follower);
                }
                else if (message.Label == "deleteFollow")
                {
                    this.iUserPostCosmosDbService.DeleteFollowerAsync(follower);
                }
                
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
