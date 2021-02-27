namespace FL.WebAPI.Core.Birds.Configuration.Models
{
    public class CosmosConfiguration
    {
        public string CosmosdbConnection { get; set; }

        public string CosmosDatabaseId { get; set; }

        public string CosmosBirdContainer { get; set; }

        public string CosmosKey { get; set; }
    }
}
