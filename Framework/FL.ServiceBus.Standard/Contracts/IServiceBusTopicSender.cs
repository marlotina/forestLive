using System.Threading.Tasks;

namespace FL.ServiceBus.Standard.Contracts
{
    public interface IServiceBusTopicSender<T>
    {
        Task SendMessage(T messageRequest, string label);

        Task CloseAsync();
    }
}
