using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using Microsoft.Azure.ServiceBus;
using System.Text;
using FL.Functions.Posts.Services;

namespace FL.Functions.Posts
{
    public class ItemPost
    {
        private readonly IPostCosmosService iPostCosmosDbService;

        public ItemPost(IPostCosmosService iPostCosmosDbService)
        {
            this.iPostCosmosDbService = iPostCosmosDbService;
        }

        [FunctionName("UserPost")]
        public void Run(
            [ServiceBusTrigger(
                "post", 
                "PostTopic", 
                Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<Model.BirdPost>(Encoding.UTF8.GetString(message.Body));

                if (post.Id != null && post.Id != Guid.Empty && post.SpecieId.HasValue)
                {
                    if (message.Label == "postCreated")
                    {
                        this.iPostCosmosDbService.CreatePostAsync(post);
                    }
                    else if (message.Label == "postDeleted")
                    {
                        this.iPostCosmosDbService.DeletePostAsync(post);
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
