using FL.Functions.BirdPost.Model;
using FL.Functions.BirdPost.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace FL.Functions.BirdPost
{
    public class PendingAddPost
    {
        private readonly IPostCosmosDbService postDbService;

        public PendingAddPost(IPostCosmosDbService postDbService)
        {
            this.postDbService = postDbService;
        }

        [FunctionName("PendingAddPost")]
        public void Run(
            [ServiceBusTrigger(
                "posts", 
                "pending", 
                Connection = "ServiceBusConnectionString")]string msg,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {msg}");
            try
            {
                var post = JsonConvert.DeserializeObject<BirdPostDto>(msg);

                if (post != null) {
                    this.postDbService.CreatePostInPendingAsync(post);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}");

            }
        }
    }
}
