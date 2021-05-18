using System;
using System.Text;
using FL.Functions.UserInteractions.Dto;
using FL.Functions.UserInteractions.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FL.Functions.UserInteractions
{
    public class UserAddVoteFunction
    {
        private readonly IUserInterationCosmosService postCosmosService;

        public UserAddVoteFunction(IUserInterationCosmosService postCosmosService)
        {
            this.postCosmosService = postCosmosService;
        }

        [FunctionName("VotePosts")]
        public void Run([ServiceBusTrigger(
                "vote",
                "voteUserSubscription",
                Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {      
                if (message.Label == "voteCreated")
                {
                    var vote = JsonConvert.DeserializeObject<VotePostDto>(Encoding.UTF8.GetString(message.Body));
                    this.postCosmosService.AddVotePostAsync(vote);
                }
                else if (message.Label == "voteDeleted")
                {
                    var vote = JsonConvert.DeserializeObject<VotePostBaseDto>(Encoding.UTF8.GetString(message.Body));
                    this.postCosmosService.DeleteVotePostAsync(vote);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");
            }
        }
    }
}
