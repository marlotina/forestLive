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
    public class UserAddPost
    {
        private readonly IPostCosmosDbService postDbService;

        public UserAddPost(IPostCosmosDbService postDbService)
        {
            this.postDbService = postDbService;
        }

        [FunctionName("UserAddPost")]
        public void Run([ServiceBusTrigger(
            "post",
            "user",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                if (message.Label == "postCreated")
                {
                    var post = JsonConvert.DeserializeObject<BirdPostDto>(Encoding.UTF8.GetString(message.Body));
                    if (post.Id != null && post.Id != Guid.Empty)
                    {
                        this.postDbService.CreatePostInUserAsync(post);
                    }
                }
                else if (message.Label == "postDeleted")
                {

                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
