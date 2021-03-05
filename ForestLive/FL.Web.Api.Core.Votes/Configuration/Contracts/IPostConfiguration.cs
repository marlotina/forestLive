using FL.Web.Api.Core.Votes.Configuration.Models;

namespace FL.Web.Api.Core.Votes.Configuration.Contracts
{
    public interface IPostConfiguration
    {
        string BirdPhotoContainer { get; }

        string Secret { get; }

        string Host { get; }

        CosmosConfig CosmosConfiguration { get; }

        ServiceBusConfig ServiceBusConfig { get;  }
    }
}
