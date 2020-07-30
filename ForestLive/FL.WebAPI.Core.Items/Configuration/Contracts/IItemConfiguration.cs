namespace FL.WebAPI.Core.Items.Configuration.Contracts
{
    public interface IItemConfiguration
    {
        string AccountName { get; }

        string AccountKey { get; }

        string ImageProfileContainer { get; }

        string Secret { get; }
    }
}
