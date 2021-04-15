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
    public class CommentVoteUserFunction
    {
        private readonly IUserInterationCosmosService postCosmosService;

        public CommentVoteUserFunction(IUserInterationCosmosService postCosmosService)
        {
            this.postCosmosService = postCosmosService;
        }

        [FunctionName("CommentVoteUserFunction")]
        public void Run([ServiceBusTrigger(
                "commentvote",
                "commentVoteUserSubscription",
                Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {      
                if (message.Label == "commentVoteCreated")
                {
                    var vote = JsonConvert.DeserializeObject<VoteCommentPostDto>(Encoding.UTF8.GetString(message.Body));
                    this.postCosmosService.AddCommentVotePostAsync(vote);
                }
                else if (message.Label == "commentVoteDeleted")
                {
                    var vote = JsonConvert.DeserializeObject<VoteCommentPostDto>(Encoding.UTF8.GetString(message.Body));
                    this.postCosmosService.DeleteCommentVotePostAsync(vote);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");
            }
        }
    }
}
