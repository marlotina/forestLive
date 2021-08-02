using System;
using System.Text;
using FL.Functions.SpeciePost.Dto;
using FL.Functions.SpeciePost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FL.Functions.SpeciePost

{
    public class SpeciePostVoteCountFunction
    {
        private readonly ISpeciePostCosmosService iBirdsCosmosService;

        public SpeciePostVoteCountFunction(ISpeciePostCosmosService iBirdsCosmosService)
        {
            this.iBirdsCosmosService = iBirdsCosmosService;
        }

        [FunctionName("VotePosts")]
        public void Run([ServiceBusTrigger(
                "vote",
                "voteSpecieSubscription",
                Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var vote = JsonConvert.DeserializeObject<VotePostBaseDto>(Encoding.UTF8.GetString(message.Body));
                if (vote.SpecieId.HasValue && vote.SpecieId.Value != Guid.Empty)
                {
                    
                    if (message.Label == "voteCreated")
                    {
                        this.iBirdsCosmosService.AddVotePostCountAsync(vote);
                    }
                    else if (message.Label == "voteDeleted")
                    {
                        this.iBirdsCosmosService.DeleteVotePostCountAsync(vote);
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
