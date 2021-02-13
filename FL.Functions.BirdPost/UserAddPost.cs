using FL.Functions.BirdPost.Model;
using FL.Functions.BirdPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace FL.Functions.BirdPost
{
    public class UserAddPost
    {
        private readonly IPostCosmosDbService postDbService;

        public UserAddPost(IPostCosmosDbService postDbService)
        {
            this.postDbService = postDbService;
        }

        [FunctionName("UserAddPost")]
        public void Run([ServiceBusTrigger(
            "posts",
            "user",
            Connection = "ServiceBusConnectionString")] string message,
            ILogger log)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<BirdPostDto>(message);

                if (post != null)
                {
                    this.postDbService.CreatePostInUserAsync(post);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");
            }
        }
    }
}
