﻿using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusPostTopicSender<T>
    {
        Task SendMessage(T messageRequest, string label);

        Task CloseAsync();
    }
}