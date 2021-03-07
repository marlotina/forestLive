using FL.Web.API.Core.Comments.Configuration.Dto;

namespace FL.Web.API.Core.Comments.Configuration.Contracts
{
    public interface IPostConfiguration
    {
        string Secret { get; }

        string Host { get; }

        CosmosConfiguration CosmosConfiguration { get; }

        ServiceBusConfig ServiceBusConfig { get;  }
    }
}
