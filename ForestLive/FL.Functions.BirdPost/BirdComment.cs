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
    public class BirdComment
    {
        private readonly IBirdsCosmosService birdsCosmosService;

        public BirdComment(IBirdsCosmosService birdsCosmosService)
        {
            this.birdsCosmosService = birdsCosmosService;
        }

        [FunctionName("BirdComment")]
        public void Run([ServiceBusTrigger(
            "comment",
            "commentUserPostTopic",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var comment = JsonConvert.DeserializeObject<BirdCommentDto>(Encoding.UTF8.GetString(message.Body));
                if (comment.Id != null && comment.Id != Guid.Empty)
                {
                    if (message.Label == "voteCreated")
                    {
                        this.birdsCosmosService.AddCommentAsync(comment);
                    }
                    else if (message.Label == "voteDeleted")
                    {
                        this.birdsCosmosService.DeleteCommentAsync(comment);
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
