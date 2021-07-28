using FL.LogTrace.Contracts.Standard;
using FL.ServiceBus.Standard.Configuration.Contracts;
using FL.ServiceBus.Standard.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FL.ServiceBus.Standard.Implementations
{
    public class ServiceBusTopicSender<T> : IServiceBusTopicSender<T> where T : class
    {
        private readonly TopicClient topicClient;
        private readonly IServiceBusConfiguration serviceBusConfiguration;
        private readonly ILogger<ServiceBusTopicSender<T>> logger;

        public ServiceBusTopicSender(
            IServiceBusConfiguration serviceBusConfiguration,
            ILogger<ServiceBusTopicSender<T>> logger)
        {
            this.serviceBusConfiguration = serviceBusConfiguration;
            this.logger = logger;
            topicClient = new TopicClient(
                this.serviceBusConfiguration.ServiceBusConfig.ConnectionString,
                this.serviceBusConfiguration.ServiceBusConfig.Topic
            );
        }

        public async Task SendMessage(T messageRequest, string label)
        {
            try
            {
                string data = JsonConvert.SerializeObject(messageRequest);
                Message message = new Message(Encoding.UTF8.GetBytes(data));
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
