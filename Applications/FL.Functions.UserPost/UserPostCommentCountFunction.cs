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
    public class UserPostCommentCountFunction
    {
        private readonly IUserPostCosmosService iUserPostCosmosService;

        public UserPostCommentCountFunction(IUserPostCosmosService iUserPostCosmosService)
        {
            this.iUserPostCosmosService = iUserPostCosmosService;
        }

        [FunctionName("BirdComment")]
        public void Run([ServiceBusTrigger(
            "comment",
            "commentUserPostSubscription",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var comment = JsonConvert.DeserializeObject<CommentDto>(Encoding.UTF8.GetString(message.Body));
                if (comment.Id != null && comment.Id != Guid.Empty)
                {
                    if (message.Label == "commentCreated")
                    {
                        this.iUserPostCosmosService.AddCommentCountAsync(comment);
                    }
                    else if (message.Label == "commentDeleted")
                    {
                        this.iUserPostCosmosService.DeleteCommentCountAsync(comment);
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
