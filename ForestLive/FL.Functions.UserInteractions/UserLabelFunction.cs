using Fl.Functions.UserInteractions.Dto;
using Fl.Functions.UserInteractions.Model;
using FL.Functions.UserInteractions.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FL.Functions.UserInteractions
{
    public class UserLabelFunction
    {
        private readonly IUserInterationCosmosService postCosmosService;

        public UserLabelFunction(IUserInterationCosmosService postCosmosService)
        {
            this.postCosmosService = postCosmosService;
        }

        [FunctionName("UserLabel")]
        public void Run([ServiceBusTrigger(
            "label",
            "labelUserPostSubscription",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                if (message.Label == "labelCreated")
                {
                    var labels = JsonConvert.DeserializeObject<IEnumerable<UserLabel>>(Encoding.UTF8.GetString(message.Body));
                    this.postCosmosService.AddLabelAsync(labels);
                }
                else if (message.Label == "labelPostRemove") 
                {
                    var labels = JsonConvert.DeserializeObject<IEnumerable<RemoveLabelDto>>(Encoding.UTF8.GetString(message.Body));
                    this.postCosmosService.RemovePostLabelAsync(labels);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
