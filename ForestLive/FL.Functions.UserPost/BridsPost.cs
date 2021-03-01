using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using FL.Functions.Pending.Services;
using FL.Functions.Pending.Model;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace FL.Functions.Pending
{
    public class PendingPost
    {
        private readonly IPendingCosmosDbService pendingCosmosDbService;

        public PendingPost(IPendingCosmosDbService pendingCosmosDbServiceç)
        {
            this.pendingCosmosDbService = pendingCosmosDbServiceç;
        }

        [FunctionName("PendingAddPost")]
        public void Run(
            [ServiceBusTrigger(
                "post", 
                "pending", 
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
                        this.pendingCosmosDbService.CreatePostInPendingAsync(post);
                    }
                    else if (message.Label == "postDeleted")
                    {
                        this.pendingCosmosDbService.DeletePostInPendingAsync(post);
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
