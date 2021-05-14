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
    public class ServiceBusLabelTopicSender<T> : IServiceBusLabelTopicSender<T> where T : class
    {
        private readonly TopicClient topicClient;
        private readonly IPostConfiguration iPostConfiguration;
        private readonly ILogger<ServiceBusLabelTopicSender<T>> logger;

        public ServiceBusLabelTopicSender(
            IPostConfiguration iPostConfiguration,
            ILogger<ServiceBusLabelTopicSender<T>> logger)
        {
            this.iPostConfiguration = iPostConfiguration;
            this.logger = logger;
            topicClient = new TopicClient(
                this.iPostConfiguration.ServiceBusConfig.ConnectionString,
                this.iPostConfiguration.ServiceBusConfig.TopicLabel
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
