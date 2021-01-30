namespace FL.WebAPI.Core.Items.Configuration.Contracts
{
    public interface IItemConfiguration
    {
        string BirdPhotoContainer { get; }

        string Secret { get; }

        string Host { get; }

        string PrimaryKey { get; }

        string Database { get; }

        string Container { get; }

        string GraphHost { get; }

        string GraphPrimaryKey { get; }

        string GraphDatabase { get; }

        string GraphContainer { get; }

        bool GraphEnableSSL { get; }
        
        int GraphPort { get; }
    }
}
