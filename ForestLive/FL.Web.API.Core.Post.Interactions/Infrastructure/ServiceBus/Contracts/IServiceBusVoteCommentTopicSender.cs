using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusVoteCommentTopicSender<T>
    {
        Task SendMessage(T messageRequest, string label);

        Task CloseAsync();
    }
}
