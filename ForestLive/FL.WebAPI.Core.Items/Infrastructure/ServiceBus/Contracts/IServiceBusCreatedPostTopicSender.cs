using FL.WebAPI.Core.Items.Domain.Entities;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusCreatedPostTopicSender<T>
    {
        Task SendMessage(T messageRequest);

        Task CloseAsync();
    }
}
