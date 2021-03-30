using Fl.Functions.UserLabel.Dto;
using FL.Functions.UserLabel.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FL.Functions.UserLabel
{
    public class UserLabelFunction
    {
        private readonly IUserLabelCosmosService iUserLabelCosmosService;

        public UserLabelFunction(IUserLabelCosmosService iUserLabelCosmosService)
        {
            this.iUserLabelCosmosService = iUserLabelCosmosService;
        }

        [FunctionName("UserLabel")]
        public void Run([ServiceBusTrigger(
            "label",
            "labelUserPostTopic",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                if (message.Label == "labelCreated")
                {
                    var labels = JsonConvert.DeserializeObject<IEnumerable<Fl.Functions.UserLabel.Model.UserLabel>>(Encoding.UTF8.GetString(message.Body));
                    this.iUserLabelCosmosService.AddLabelAsync(labels);
                }
                else if (message.Label == "labelPostRemove") 
                {
                    var labels = JsonConvert.DeserializeObject<IEnumerable<RemoveLabelDto>>(Encoding.UTF8.GetString(message.Body));
                    this.iUserLabelCosmosService.RemovePostLabelAsync(labels);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}
