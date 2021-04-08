using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Bird.Pending.Configuration.Contracts;
using FL.Web.API.Core.Bird.Pending.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Infrastructure.ServiceBus.Implementations
{
    public class ServiceBusLabelTopicSender<T> : IServiceBusLabelTopicSender<T> where T : class
    {
        private readonly TopicClient topicClient;
        private readonly IBirdPendingConfiguration iBirdsConfiguration;
        private readonly ILogger<ServiceBusPostTopicSender<T>> logger;

        public ServiceBusLabelTopicSender(
            IBirdPendingConfiguration iBirdsConfiguration,
            ILogger<ServiceBusPostTopicSender<T>> logger)
        {
            this.iBirdsConfiguration = iBirdsConfiguration;
            this.logger = logger;
            topicClient = new TopicClient(
                this.iBirdsConfiguration.ServiceBusConfig.ConnectionString,
                this.iBirdsConfiguration.ServiceBusConfig.TopicLabel
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
