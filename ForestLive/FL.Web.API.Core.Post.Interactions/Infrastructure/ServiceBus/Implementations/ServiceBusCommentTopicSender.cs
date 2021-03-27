using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Post.Interactions.Configuration.Contracts;
using FL.Web.API.Core.Post.Interactions.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Infrastructure.ServiceBus.Implementations
{
    public class ServiceBusCommentTopicSender<T> : IServiceBusCommentTopicSender<T> where T : class
    {
        private readonly TopicClient topicClient;
        private readonly IPostConfiguration itemConfiguration;
        private readonly ILogger<ServiceBusCommentTopicSender<T>> logger;

        public ServiceBusCommentTopicSender(
            IPostConfiguration itemConfiguration,
            ILogger<ServiceBusCommentTopicSender<T>> logger)
        {
            this.itemConfiguration = itemConfiguration;
            this.logger = logger;
            topicClient = new TopicClient(
                this.itemConfiguration.ServiceBusConfig.ConnectionString,
                this.itemConfiguration.ServiceBusConfig.TopicComment
            );
        }

        public async Task SendMessage(T messageRequest, string label)
        {
            try
            {
                string data = JsonConvert.SerializeObject(messageRequest);
                var message = new Message(Encoding.UTF8.GetBytes(data));
                message.Label = label;
                await topicClient.SendAsync(message);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
        public async Task CloseAsync()
        {
            await this.topicClient.CloseAsync();
        }

        public bool IsClosedOrClosing => this.topicClient.IsClosedOrClosing;
    }
}
