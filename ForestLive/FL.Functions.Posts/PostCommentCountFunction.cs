using FL.Functions.Posts.Dto;
using FL.Functions.Posts.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.Posts
{
    public class PostCommentCountFunction
    {
        private readonly IPostCosmosService iPostDbService;

        public PostCommentCountFunction(IPostCosmosService iPostDbService)
        {
            this.iPostDbService = iPostDbService;
        }

        [FunctionName("CommentPost")]
        public void Run([ServiceBusTrigger(
            "comment",
            "commentPostSubscription",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<CommentBaseDto>(Encoding.UTF8.GetString(message.Body));
               
                if (post.Id != null && post.Id != Guid.Empty)
                {
                    if (message.Label == "commentCreated")
                    {
                        this.iPostDbService.AddCommentPostCountAsync(post);
                    }
                    else if (message.Label == "commentDeleted")
                    {
                        this.iPostDbService.DeleteCommentPostCountAsync(post);
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