using FL.LogTrace.Contracts.Standard;
using FL.ServiceBus.Standard.Configuration.Contracts;
using FL.ServiceBus.Standard.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using Microsoft.Azure.ServiceBus.InteropExtensions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FL.ServiceBus.Standard.Implementations
{
    public class ServiceBusTopicSubscription<T> : IServiceBusTopicSubscription<T>
    {
        private readonly IServiceBusConfiguration serviceBusConfiguration;
        private readonly SubscriptionClient subscriptionClient;
        private const string TOPIC_PATH = "mytopic";
        private const string SUBSCRIPTION_NAME = "mytopicsubscription";
        private readonly ILogger<ServiceBusTopicSubscription<T>> logger;

        public ServiceBusTopicSubscription(
            IServiceBusConfiguration serviceBusConfiguration,
            ILogger<ServiceBusTopicSubscription<T>> logger)
        {
            this.serviceBusConfiguration = serviceBusConfiguration;
            this.logger = logger;

            subscriptionClient = new SubscriptionClient(
                this.serviceBusConfiguration.ServiceBusConfig.ConnectionString,
                this.serviceBusConfiguration.ServiceBusConfig.Topic,
                SUBSCRIPTION_NAME);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            //subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

            this.subscriptionClient.RegisterMessageHandler(
                async (message, cancellationToken) =>
                {
                    var msg =  JsonConvert.DeserializeObject<T>(GetBody(message));
                    if (msg != null)
                    {
                        await handlerAsync(msg, cancellationToken);
                    }
                    else if (this.subscriptionClient.ReceiveMode == Microsoft.Azure.ServiceBus.ReceiveMode.PeekLock)
                    {
                        await this.subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                    }
                }, options);
        }

        private string GetBody(Message source)
        {
            if (source == null)
                return null;

            string result;
            if (!string.IsNullOrWhiteSpace(source.ContentType)
                 && source.ContentType.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
            {
                result = Encoding.UTF8.GetString(source.Body);
            }
            else
            {
                result = source.GetBody<string>();
            }

            return result;
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var myPayload = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
            //_processData.Process(myPayload);
            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            this.logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            this.logger.LogDebug($"- Endpoint: {context.Endpoint}");
            this.logger.LogDebug($"- Entity Path: {context.EntityPath}");
            this.logger.LogDebug($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }

        public async Task CloseSubscriptionClientAsync()
        {
            await subscriptionClient.CloseAsync();
        }
    }
}
