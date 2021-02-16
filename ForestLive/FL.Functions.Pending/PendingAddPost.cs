using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using FL.Functions.Pending.Services;
using FL.Functions.Pending.Model;

namespace FL.Functions.Pending
{
    public class PendingAddPost
    {
        private readonly IPendingCosmosDbService pendingCosmosDbService�;

        public PendingAddPost(IPendingCosmosDbService pendingCosmosDbService�)
        {
            this.pendingCosmosDbService� = pendingCosmosDbService�;
        }

        [FunctionName("PendingAddPost")]
        public void Run(
            [ServiceBusTrigger(
                "posts", 
                "pending", 
                Connection = "ServiceBusConnectionString")] string message,
            ILogger log)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<BirdPostDto>(message);

                if (post != null) {
                    this.pendingCosmosDbService�.CreatePostInPendingAsync(post);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
