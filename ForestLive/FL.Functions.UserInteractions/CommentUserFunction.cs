using FL.Functions.UserInteractions.Dto;
using FL.Functions.UserInteractions.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FL.Functions.UserInteractions
{
    public class CommentUserFunction
    {
        private readonly IUserLabelCosmosService userLabelCosmosService;

        public CommentUserFunction(IUserLabelCosmosService userLabelCosmosService)
        {
            this.userLabelCosmosService = userLabelCosmosService;
        }

        [FunctionName("CommentUser")]
        public void Run([ServiceBusTrigger(
            "comment",
            "commentUserTopic",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                if (message.Label == "commentCreated")
                {
                    var comment = JsonConvert.DeserializeObject<BirdCommentDto>(Encoding.UTF8.GetString(message.Body));
                    this.userLabelCosmosService.AddCommentPostAsync(comment);
                }
                else if (message.Label == "commentDeleted") 
                {
                    var comment = JsonConvert.DeserializeObject<BirdCommentDto>(Encoding.UTF8.GetString(message.Body));
                    this.userLabelCosmosService.DeleteCommentPostAsync(comment);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Couldn't insert item. Exception thrown: {ex.Message}. Messagee##={message}");

            }
        }
    }
}