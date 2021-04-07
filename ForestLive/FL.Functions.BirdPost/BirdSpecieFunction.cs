using FL.Functions.BirdPost.Dto;
using FL.Functions.BirdPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.BirdPost
{
    public class BirdSpecieFunction
    {
        private readonly IBirdsCosmosService iBirdsCosmosService;

        public BirdSpecieFunction(IBirdsCosmosService iBirdsCosmosService)
        {
            this.iBirdsCosmosService = iBirdsCosmosService;
        }

        [FunctionName("FunctionBirdSpecie")]
        public void Run([ServiceBusTrigger(
            "updatespecie",
            "SpecieChangeBirdSubscription",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<Dto.BirdPost>(Encoding.UTF8.GetString(message.Body));
                if (post.SpecieId != null && post.SpecieId != Guid.Empty)
                {
                    if (post.Id != null && post.Id != Guid.Empty)
                    {
                        if (message.Label == "assignSpecie")
                        {
                            this.iBirdsCosmosService.CreatePostAsync(post);
                        }
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
