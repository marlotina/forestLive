using FL.Functions.SpeciePost.Dto;
using FL.Functions.SpeciePost.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.SpeciePost
{
    public class SpeciePostCommentCountFunction
    {
        private readonly ISpeciePostCosmosService iBirdsCosmosService;

        public SpeciePostCommentCountFunction(ISpeciePostCosmosService iBirdsCosmosService)
        {
            this.iBirdsCosmosService = iBirdsCosmosService;
        }

        [FunctionName("FunctionBirdComment")]
        public void Run([ServiceBusTrigger(
            "comment",
            "commentSpecieSubscription",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var comment = JsonConvert.DeserializeObject<CommentBaseDto>(Encoding.UTF8.GetString(message.Body));
                if (comment.SpecieId != null && comment.SpecieId != Guid.Empty)
                {
                    if (comment.Id != null && comment.Id != Guid.Empty)
                    {
                        if (message.Label == "commentCreated")
                        {
                            this.iBirdsCosmosService.AddCommentCountAsync(comment);
                        }
                        else if (message.Label == "commentDeleted")
                        {
                            this.iBirdsCosmosService.DeleteCommentCountAsync(comment);
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
