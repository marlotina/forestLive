using System;
using FL.Functions.Post.Model;
using FL.Functions.Post.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FL.Functions.Votes
{
    public class VotePost
    {
        private readonly IPostCosmosService postDbService;

        public VotePost(IPostCosmosService postDbService)
        {
            this.postDbService = postDbService;
        }

        [FunctionName("AddVote")]
        public void Run([ServiceBusTrigger(
                "votes",
                "vote",
                Connection = "ServiceBusConnectionString")] string message,
            ILogger log)
        {
            try
            {
                var vote = JsonConvert.DeserializeObject<VotePostDto>(message);

                if (vote != null)
                {
                    this..CreateVoteInUserAsync(vote);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
