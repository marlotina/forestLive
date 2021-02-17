using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Implementations
{
    public class ServiceBusDeletedPostTopicSender<T> : IServiceBusDeletedPostTopicSender<T> where T : class
    {
        private readonly TopicClient topicClient;
        private readonly IItemConfiguration itemConfiguration;
        private readonly ILogger<ServiceBusDeletedPostTopicSender<T>> logger;

        public ServiceBusDeletedPostTopicSender(
            IItemConfiguration itemConfiguration,
            ILogger<ServiceBusDeletedPostTopicSender<T>> logger)
        {
            this.itemConfiguration = itemConfiguration;
            this.logger = logger;
            topicClient = new TopicClient(
                this.itemConfiguration.ServiceBusConfig.ConnectionString,
                this.itemConfiguration.ServiceBusConfig.TopicPostDeleted
            );
        }

        public async Task SendMessage(T messageRequest)
        {
            try
            {
                string data = JsonConvert.SerializeObject(messageRequest);
                Message message = new Message(Encoding.UTF8.GetBytes(data));
                
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
