using FL.WebAPI.Core.Users.Configuration.Dto;

namespace FL.WebAPI.Core.Users.Configuration.Contracts
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
