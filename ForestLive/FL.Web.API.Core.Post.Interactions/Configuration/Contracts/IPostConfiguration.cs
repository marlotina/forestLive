using FL.Web.API.Core.Post.Interactions.Configuration.Dto;
using RestSharp;
using System;

namespace FL.Web.API.Core.Post.Interactions.Configuration.Contracts
{
    public interface IPostConfiguration
    {
        CosmosConfiguration CosmosConfiguration { get; }

        ServiceBusConfig ServiceBusConfig { get;  }

        string VoteApiDomain { get; }

        string VoteUrlService { get; }

        string UserUrlService { get; }

        string UserApiDomain { get; }
    }
}
