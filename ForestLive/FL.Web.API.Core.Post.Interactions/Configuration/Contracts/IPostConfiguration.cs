using FL.Web.API.Core.Post.Interactions.Configuration.Dto;

namespace FL.Web.API.Core.Post.Interactions.Configuration.Contracts
{
    public interface IPostConfiguration
    {
        string Secret { get; }

        string Host { get; }

        CosmosConfiguration CosmosConfiguration { get; }

        ServiceBusConfig ServiceBusConfig { get;  }
    }
}
