using FL.LogTrace.Contracts.Standard;
using FL.Web.Api.Core.Votes.Configuration.Contracts;
using FL.Web.Api.Core.Votes.Infrastructure.ServiceBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Infrastructure.ServiceBus.Implementations
{
    public class ServiceBusVotePostTopicSender<T> : IServiceBusVotePostTopicSender<T> where T : class
    {
        private readonly TopicClient topicClient;
        private readonly IPostConfiguration itemConfiguration;
        private readonly ILogger<ServiceBusVotePostTopicSender<T>> logger;

        public ServiceBusVotePostTopicSender(
            IPostConfiguration itemConfiguration,
            ILogger<ServiceBusVotePostTopicSender<T>> logger)
        {
            this.itemConfiguration = itemConfiguration;
            this.logger = logger;
            topicClient = new TopicClient(
                this.itemConfiguration.ServiceBusConfig.ConnectionString,
                this.itemConfiguration.ServiceBusConfig.TopicVote
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

