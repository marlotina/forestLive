using FL.Functions.UserInteractions.Dto;
using FL.Functions.UserInteractions.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.BirdPost
{
    public class UserAddFollowFunction
    {
        private readonly IUserInterationCosmosService iUserInterationCosmosService;

        public UserAddFollowFunction(IUserInterationCosmosService iUserInterationCosmosService)
        {
            this.iUserInterationCosmosService = iUserInterationCosmosService;
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
                    this.iUserInterationCosmosService.AddFollowerAsync(follower);
                }
                else if (message.Label == "deleteFollow")
                {
                    this.iUserInterationCosmosService.DeleteFollowerAsync(follower);
                }
                
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
