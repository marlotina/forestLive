using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using FL.Functions.Pending.Services;
using FL.Functions.Pending.Model;
using Microsoft.Azure.ServiceBus;
using System.Text;
using FL.Functions.UserPost.Services;
using FL.Functions.UserPost.Model;

namespace FL.Functions.UserPost
{
    public class UserPost
    {
        private readonly IUserPostCosmosDbService userPostCosmosDbService;

        public UserPost(IUserPostCosmosDbService userPostCosmosDbService)
        {
            this.userPostCosmosDbService = userPostCosmosDbService;
        }

        [FunctionName("UserPost")]
        public void Run(
            [ServiceBusTrigger(
                "post", 
                "UserPostTopic", 
                Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<BirdPostDto>(Encoding.UTF8.GetString(message.Body));

                if (post.Id != null && post.Id != Guid.Empty)
                {
                    if (message.Label == "postCreated")
                    {
                        this.userPostCosmosDbService.CreatePostInPendingAsync(post);
                    }
                    else if (message.Label == "postDeleted")
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