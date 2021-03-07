using FL.Functions.Posts.Model;
using FL.Functions.Posts.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FL.Functions.Posts
{
    public class PostComment
    {
        private readonly IPostCosmosService postDbService;

        public PostComment(IPostCosmosService postDbService)
        {
            this.postDbService = postDbService;
        }

        [FunctionName("CommentPost")]
        public void Run([ServiceBusTrigger(
            "comment",
            "PostCommentTopic",
            Connection = "ServiceBusConnectionString")] Message message,
            ILogger log)
        {
            try
            {
                var post = JsonConvert.DeserializeObject<BirdComment>(Encoding.UTF8.GetString(message.Body));
                if (post.Id != null && post.Id != Guid.Empty)
                {
                    if (message.Label == "commentCreated")
                    {
                        this.postDbService.AddCommentPostAsync(post);
                    }
                    else if (message.Label == "commentDeleted")
                    {
                        this.postDbService.DeleteCommentPostAsync(post);
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
