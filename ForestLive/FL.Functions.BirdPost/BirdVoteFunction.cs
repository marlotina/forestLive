using FL.Functions.BirdPost.Dto;
using FL.Functions.BirdPost.Model;
using FL.Functions.BirdPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.BirdPost
{
    public class BirdVoteFunction
    {
        private readonly IBirdsCosmosService birdsCosmosService;

        public BirdVoteFunction(IBirdsCosmosService birdsCosmosService)
        {
            this.birdsCosmosService = birdsCosmosService;
        }

        [FunctionName("FunctionBirdVote")]
        public void Run([ServiceBusTrigger(
            "vote",
            "voteBirdPostTopic",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var vote = JsonConvert.DeserializeObject<VotePostDto>(Encoding.UTF8.GetString(message.Body));
                if (vote.Id != null && vote.Id != Guid.Empty)
                {
                    if (message.Label == "voteCreated")
                    {
                        this.birdsCosmosService.AddVoteAsync(vote);
                    }
                    else if (message.Label == "voteDeleted")
                    {
                        this.birdsCosmosService.DeleteVoteAsync(vote);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
