using System;
using System.Text;
using FL.Functions.Posts.Model;
using FL.Functions.Posts.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FL.Functions.Posts
{
    public class VotePost
    {
        private readonly IPostCosmosService postCosmosService;

        public VotePost(IPostCosmosService postCosmosService)
        {
            this.postCosmosService = postCosmosService;
        }

        [FunctionName("VotePosts")]
        public void Run([ServiceBusTrigger(
                "votes",
                "vote",
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
                        this.postCosmosService.AddVotePostAsync(vote);
                    }
                    else if (message.Label == "voteDeleted")
                    {
                        this.postCosmosService.DeleteVotePostAsync(vote);
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
