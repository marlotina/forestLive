﻿namespace FL.WebAPI.Core.Birds.Configuration.Models
{
    public class CosmosConfiguration
    {
        public string CosmosDatabaseId { get; set; }

        public string CosmosBirdContainer { get; set; }

        public string CosmosUserContainer { get; set; }

        public string CosmosPostContainer { get; set; }
    }
}
