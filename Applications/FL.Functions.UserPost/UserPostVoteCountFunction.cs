using FL.Functions.UserPost.Dto;
using FL.Functions.UserPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.BirdPost
{
    public class UserPostVoteCountFunction
    {
        private readonly IUserPostCosmosService iUserPostCosmosDbService;

        public UserPostVoteCountFunction(IUserPostCosmosService iUserPostCosmosDbService)
        {
            this.iUserPostCosmosDbService = iUserPostCosmosDbService;
        }

        [FunctionName("UserVote")]
        public void Run([ServiceBusTrigger(
            "vote",
            "voteUserPostSubscription",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var vote = JsonConvert.DeserializeObject<VotePostDto>(Encoding.UTF8.GetString(message.Body));
                
                if (message.Label == "voteCreated")
                {
                    this.iUserPostCosmosDbService.AddVoteCountAsync(vote);
                }
                else if (message.Label == "voteDeleted")
                {
                    this.iUserPostCosmosDbService.DeleteVoteCountAsync(vote);
                }
                
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
