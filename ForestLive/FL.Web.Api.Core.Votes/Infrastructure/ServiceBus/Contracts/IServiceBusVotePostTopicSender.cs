using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Infrastructure.ServiceBus.Contracts
{
    public interface IServiceBusVotePostTopicSender<T>
    {
        Task SendMessage(T messageRequest, string label);

        Task CloseAsync();
    }
}
