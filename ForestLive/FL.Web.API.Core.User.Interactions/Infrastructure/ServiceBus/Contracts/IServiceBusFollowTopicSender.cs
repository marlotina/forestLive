using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusFollowTopicSender<T>
    {
        Task SendMessage(T messageRequest, string label);

        Task CloseAsync();
    }
}
