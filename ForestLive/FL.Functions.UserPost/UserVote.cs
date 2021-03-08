using FL.Functions.BirdPost.Model;
using FL.Functions.BirdPost.Services;
using FL.Functions.UserPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.BirdPost
{
    public class UserVote
    {
        private readonly IUserPostCosmosService userPostCosmosService;

        public UserVote(IBirdsCosmosService postDbService)
        {
            this.userPostCosmosService = postDbService;
        }

        [FunctionName("BirdPost")]
        public void Run([ServiceBusTrigger(
            "post",
            "BirdPostTopic",
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
                        this.userPostCosmosService.AddVoteAsync(vote);
                    }
                    else if (message.Label == "voteDeleted")
                    {
                        this.userPostCosmosService.DeleteVoteAsync(vote);
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
