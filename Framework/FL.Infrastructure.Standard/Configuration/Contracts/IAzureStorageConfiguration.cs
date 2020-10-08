namespace FL.Infrastructure.Standard.Configuration.Contracts
{
    public interface IAzureStorageConfiguration
    {
        string AccountName { get; }

        string AccountKey { get; }
    }
}
