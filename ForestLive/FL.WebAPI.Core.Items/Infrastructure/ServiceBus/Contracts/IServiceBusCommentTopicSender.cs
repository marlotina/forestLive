using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusCommentTopicSender<T>
    {
        Task SendMessage(T messageRequest, string label);

        Task CloseAsync();
    }
}
