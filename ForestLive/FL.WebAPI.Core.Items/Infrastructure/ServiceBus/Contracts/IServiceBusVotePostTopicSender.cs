using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusVotePostTopicSender<T>
    {
        Task SendMessage(T messageRequest);

        Task CloseAsync();
    }
}
