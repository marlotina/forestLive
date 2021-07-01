using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Configuration.Contracts;
using FL.WebAPI.Core.User.Interactions.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Interactions.Infrastructure.ServiceBus.Implementations
{
    public class ServiceBusFollowerTopicSender<T> : IServiceBusFollowerTopicSender<T> where T : class
    {
        private readonly TopicClient topicClient;
        private readonly IUserInteractionsConfiguration iPostConfiguration;
        private readonly ILogger<ServiceBusFollowerTopicSender<T>> logger;

        public ServiceBusFollowerTopicSender(
            IUserInteractionsConfiguration iPostConfiguration,
            ILogger<ServiceBusFollowerTopicSender<T>> logger)
        {
            this.iPostConfiguration = iPostConfiguration;
            this.logger = logger;
            topicClient = new TopicClient(
                this.iPostConfiguration.ServiceBusConfig.ConnectionString,
                this.iPostConfiguration.ServiceBusConfig.TopicFollow
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
                logger.LogError($"{e.Message}, {JsonConvert.SerializeObject(messageRequest)}");
            }
        }
        public async Task CloseAsync()
        {
            await this.topicClient.CloseAsync();
        }

        public bool IsClosedOrClosing => this.topicClient.IsClosedOrClosing;
    }
}
