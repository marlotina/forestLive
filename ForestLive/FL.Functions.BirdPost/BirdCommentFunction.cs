using FL.Functions.BirdPost.Dto;
using FL.Functions.BirdPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.BirdPost
{
    public class BirdCommentFunction
    {
        private readonly IBirdsCosmosService birdsCosmosService;

        public BirdCommentFunction(IBirdsCosmosService birdsCosmosService)
        {
            this.birdsCosmosService = birdsCosmosService;
        }

        [FunctionName("FunctionBirdComment")]
        public void Run([ServiceBusTrigger(
            "comment",
            "commentBirdTopic",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var comment = JsonConvert.DeserializeObject<BirdCommentDto>(Encoding.UTF8.GetString(message.Body));
                if (comment.Id != null && comment.Id != Guid.Empty)
                {
                    if (message.Label == "commentCreated")
                    {
                        this.birdsCosmosService.AddCommentAsync(comment);
                    }
                    else if (message.Label == "commentDeleted")
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
