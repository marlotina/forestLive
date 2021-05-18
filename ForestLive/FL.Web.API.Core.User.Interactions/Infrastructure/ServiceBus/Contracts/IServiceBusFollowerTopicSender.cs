using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Interactions.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusFollowerTopicSender<T>
    {
        Task SendMessage(T messageRequest, string label);

        Task CloseAsync();
    }
}
