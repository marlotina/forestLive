using System;
using FL.Functions.Pending.Model;
using FL.Functions.Pending.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FL.Functions.Pending
{
    public class RemovePending
    {

        private readonly IPendingCosmosDbService pendingCosmosDbService;

        public RemovePending(IPendingCosmosDbService pendingCosmosDbService)
        {
            this.pendingCosmosDbService = pendingCosmosDbService;
        }

        [FunctionName("RemovePending")]
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

                if (post != null)
                {
                    this.pendingCosmosDbService.DeletePostInPendingAsync(post);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
