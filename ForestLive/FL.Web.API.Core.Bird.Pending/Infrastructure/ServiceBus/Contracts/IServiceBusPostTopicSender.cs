using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusPostTopicSender<T>
    {
        Task SendMessage(T messageRequest, string label);

        Task CloseAsync();
    }
}
