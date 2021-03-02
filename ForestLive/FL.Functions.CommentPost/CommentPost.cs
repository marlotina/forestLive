using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using Microsoft.Azure.ServiceBus;
using System.Text;
using FL.Functions.UserPost.Services;
using FL.Functions.UserPost.Model;

namespace FL.Functions.UserPost
{
    public class CommentPost
    {
        private readonly ICommentPostCosmosDbService userPostCosmosDbService;

        public CommentPost(ICommentPostCosmosDbService userPostCosmosDbService)
        {
            this.userPostCosmosDbService = userPostCosmosDbService;
        }

        [FunctionName("UserPost")]
        public void Run(
            [ServiceBusTrigger(
                "post", 
                "UserCommentTopic", 
                Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<CommentPostDto>(Encoding.UTF8.GetString(message.Body));

                if (post.Id != null && post.Id != Guid.Empty)
                {
                    if (message.Label == "commentCreated")
                    {
                        this.userPostCosmosDbService.CreatePostInPendingAsync(post);
                    }
                    else if (message.Label == "commentDeleted")
                    {
                        this.userPostCosmosDbService.DeletePostInPendingAsync(post);
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
