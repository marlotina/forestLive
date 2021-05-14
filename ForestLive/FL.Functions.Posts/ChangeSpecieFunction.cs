using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using Microsoft.Azure.ServiceBus;
using System.Text;
using FL.Functions.Posts.Services;
using FL.Functions.Posts.Model;

namespace FL.Functions.UserPost
{
    public class ChangeSpecieFunction
    {
        private readonly IPostCosmosService iPostCosmosDbService;

        public ChangeSpecieFunction(IPostCosmosService iPostCosmosDbService)
        {
            this.iPostCosmosDbService = iPostCosmosDbService;
        }

        [FunctionName("UserSpecie")]
        public void Run(
            [ServiceBusTrigger(
                "updatespecie",
                "specieChangePostSubscription", 
                Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<BirdPost>(Encoding.UTF8.GetString(message.Body));

                if (post.Id != null && post.Id != Guid.Empty)
                {
                    this.iPostCosmosDbService.UpdatePostAsync(post);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
