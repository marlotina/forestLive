using System;
using System.Text;
using FL.Functions.BirdPost.Dto;
using FL.Functions.BirdPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FL.Functions.BirdPost

{
    public class VotePostFunction
    {
        private readonly IBirdsCosmosService birdsCosmosService;

        public VotePostFunction(IBirdsCosmosService birdsCosmosService)
        {
            this.birdsCosmosService = birdsCosmosService;
        }

        [FunctionName("VotePosts")]
        public void Run([ServiceBusTrigger(
                "vote",
                "voteBirdTopic",
                Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var vote = JsonConvert.DeserializeObject<VotePostDto>(Encoding.UTF8.GetString(message.Body));
                if (vote.SpecieId.HasValue && vote.SpecieId.Value != Guid.Empty)
                {
                    if (vote.Id != null && vote.Id != Guid.Empty)
                    {
                        if (message.Label == "voteCreated")
                        {
                            this.birdsCosmosService.AddVotePostAsync(vote);
                        }
                        else if (message.Label == "voteDeleted")
                        {
                            this.birdsCosmosService.DeleteVotePostAsync(vote);
                        }
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
