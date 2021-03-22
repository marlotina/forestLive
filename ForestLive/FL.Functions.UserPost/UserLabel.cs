using FL.Functions.UserPost.Dto;
using FL.Functions.UserPost.Model;
using FL.Functions.UserPost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FL.Functions.BirdPost
{
    public class UserLabel
    {
        private readonly IUserPostCosmosService userPostCosmosService;

        public UserLabel(IUserPostCosmosService userPostCosmosService)
        {
            this.userPostCosmosService = userPostCosmosService;
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
                var labels = JsonConvert.DeserializeObject<List<LabelDto>>(Encoding.UTF8.GetString(message.Body));
                if (labels != null && labels.Any())
                {
                    if (message.Label == "labelCreated")
                    {

                        this.userPostCosmosService.AddLabelAsync(labels);
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
