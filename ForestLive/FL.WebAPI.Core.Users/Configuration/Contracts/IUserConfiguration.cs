using FL.WebAPI.Core.Users.Configuration.Dto;

namespace FL.WebAPI.Core.Users.Configuration.Contracts
{
    public interface IUserConfiguration
    {
        string AccountName { get; }

        string AccountKey { get; }

        string ImageProfileContainer { get; }

        string SendgridApiKey { get; }

        string SupportName { get; }

        string SupportEmail { get; }

        string Secret { get; }

        string UrlConfirmEmail { get; }

        string ConnectionStringUsersSite { get; }

        string UrlForgotPasswordEmail { get; }

        CosmosConfiguration CosmosConfiguration { get; }
    }
}
