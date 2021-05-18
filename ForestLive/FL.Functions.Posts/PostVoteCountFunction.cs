using System;
using System.Text;
using FL.Functions.Posts.Dto;
using FL.Functions.Posts.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FL.Functions.Posts
{
    public class PostVoteCountFunction
    {
        private readonly IPostCosmosService iPostCosmosService;

        public PostVoteCountFunction(IPostCosmosService iPostCosmosService)
        {
            this.iPostCosmosService = iPostCosmosService;
        }

        [FunctionName("VotePosts")]
        public void Run([ServiceBusTrigger(
                "vote",
                "votePostTopic",
                Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var vote = JsonConvert.DeserializeObject<VotePostBaseDto>(Encoding.UTF8.GetString(message.Body));
               
                
                if (message.Label == "voteCreated")
                {
                    this.iPostCosmosService.AddVotePostCountAsync(vote);
                }
                else if (message.Label == "voteDeleted")
                {
                    this.iPostCosmosService.DeleteVotePostCountAsync(vote);
                }
                
                
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
