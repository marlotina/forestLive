using System.Threading.Tasks;

namespace FL.Web.API.Core.Comments.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusCommentTopicSender<T>
    {
        Task SendMessage(T messageRequest, string label);

        Task CloseAsync();
    }
}
