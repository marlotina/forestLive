using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Birds.Configuration.Contracts;
using FL.WebAPI.Core.Birds.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Infrastructure.ServiceBus.Implementations
{
    public class ServiceBusPostTopicSender<T> : IServiceBusPostTopicSender<T> where T : class
    {
        private readonly TopicClient topicClient;
        private readonly IBirdsConfiguration iBirdsConfiguration;
        private readonly ILogger<ServiceBusPostTopicSender<T>> logger;

        public ServiceBusPostTopicSender(
            IBirdsConfiguration iBirdsConfiguration,
            ILogger<ServiceBusPostTopicSender<T>> logger)
        {
            this.iBirdsConfiguration = iBirdsConfiguration;
            this.logger = logger;
            topicClient = new TopicClient(
                this.iBirdsConfiguration.ServiceBusConfig.ConnectionString,
                this.iBirdsConfiguration.ServiceBusConfig.TopicPost
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
