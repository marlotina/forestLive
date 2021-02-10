using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FL.ServiceBus.Standard.Contracts
{
    public interface IServiceBusTopicSubscription<T>
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
        Task CloseSubscriptionClientAsync();
    }
}
