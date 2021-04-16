using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Configuration.Contracts;
using FL.Web.API.Core.User.Interactions.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Infrastructure.ServiceBus.Implementations
{
    public class ServiceBusFollowTopicSender<T> : IServiceBusFollowTopicSender<T> where T : class
    {
        private readonly TopicClient topicClient;
        private readonly IVoteConfiguration iBirdsConfiguration;
        private readonly ILogger<ServiceBusFollowTopicSender<T>> logger;

        public ServiceBusFollowTopicSender(
            IVoteConfiguration iBirdsConfiguration,
            ILogger<ServiceBusFollowTopicSender<T>> logger)
        {
            this.iBirdsConfiguration = iBirdsConfiguration;
            this.logger = logger;
            topicClient = new TopicClient(
                this.iBirdsConfiguration.ServiceBusConfig.ConnectionString,
                this.iBirdsConfiguration.ServiceBusConfig.TopicFollow
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
