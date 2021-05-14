using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using Microsoft.Azure.ServiceBus;
using System.Text;
using FL.Functions.UserPost.Services;
using FL.Functions.UserPost.Model;

namespace FL.Functions.UserPost
{
    public class UserSpecie
    {
        private readonly IUserPostCosmosService iUserPostCosmosDbService;

        public UserSpecie(IUserPostCosmosService iUserPostCosmosDbService)
        {
            this.iUserPostCosmosDbService = iUserPostCosmosDbService;
        }

        [FunctionName("UserSpecie")]
        public void Run(
            [ServiceBusTrigger(
                "updatespecie",
                "specieChangeUserSubscription", 
                Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<Model.BirdPost>(Encoding.UTF8.GetString(message.Body));

                if (post.Id != null && post.Id != Guid.Empty)
                {
                    this.iUserPostCosmosDbService.UpdatePostAsync(post);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
