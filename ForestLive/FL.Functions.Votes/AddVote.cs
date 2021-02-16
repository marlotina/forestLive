using System;
using FL.Functions.Votes.Model;
using FL.Functions.Votes.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FL.Functions.Votes
{
    public class AddVote
    {
        private readonly IVoteCosmosDbService voteCosmosDbService;

        public AddVote(IVoteCosmosDbService voteCosmosDbService)
        {
            this.voteCosmosDbService = voteCosmosDbService;
        }

        [FunctionName("AddVote")]
        public void Run([ServiceBusTrigger(
                "votes",
                "user",
                Connection = "ServiceBusConnectionString")] string message,
            ILogger log)
        {
            try
            {
                var vote = JsonConvert.DeserializeObject<VotePostDto>(message);

                if (vote != null)
                {
                    this.voteCosmosDbService.CreateVoteInUserAsync(vote);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
