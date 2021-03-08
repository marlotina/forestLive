using FL.Functions.UserPost.Dto;
using FL.Functions.UserPost.Model;
using FL.Functions.UserPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.BirdPost
{
    public class UserComment
    {
        private readonly IUserPostCosmosService userPostCosmosService;

        public UserComment(IUserPostCosmosService userPostCosmosService)
        {
            this.userPostCosmosService = userPostCosmosService;
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
                    if (message.Label == "commentCreated")
                    {
                        this.userPostCosmosService.AddCommentAsync(comment);
                    }
                    else if (message.Label == "commentDeleted")
                    {
                        this.userPostCosmosService.DeleteCommentAsync(comment);
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
