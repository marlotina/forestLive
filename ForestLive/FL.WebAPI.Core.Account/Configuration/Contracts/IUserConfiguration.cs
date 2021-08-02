using FL.WebAPI.Core.Account.Configuration.Dto;

namespace FL.WebAPI.Core.Account.Configuration.Contracts
{
    public interface IUserConfiguration
    {
        string ImageProfileContainer { get; }

        string Secret { get; }

        string ConnectionStringUsersSite { get; }

        CosmosConfiguration CosmosConfiguration { get; }

        string  UserInteractionApiDomain { get; }

        string FollowUrlService { get; }
    }
}
