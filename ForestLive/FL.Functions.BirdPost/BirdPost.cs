using FL.Functions.BirdPost.Model;
using FL.Functions.BirdPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.BirdPost
{
    public class BirdPost
    {
        private readonly IBirdsCosmosService postDbService;

        public BirdPost(IBirdsCosmosService postDbService)
        {
            this.postDbService = postDbService;
        }

        [FunctionName("BirdPost")]
        public void Run([ServiceBusTrigger(
            "post",
            "BirdPostTopic",
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
                        this.postDbService.CreatePostAsync(post);
                    }
                    else if (message.Label == "postDeleted")
                    {
                        this.postDbService.DeletePostAsync(post);
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
