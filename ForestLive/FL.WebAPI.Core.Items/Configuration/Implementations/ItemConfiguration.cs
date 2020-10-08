using FL.WebAPI.Core.Items.Configuration.Contracts;
using Microsoft.Extensions.Configuration;
using System;

namespace FL.WebAPI.Core.Items.Configuration.Implementations
{
    public class ItemConfiguration : IItemConfiguration
    {
        private readonly IConfiguration configuration;

        public ItemConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string BirthPhotoContainer => this.configuration.GetSection("BirthPhotoContainer").Get<string>();

        public string Secret => this.configuration.GetSection("Secret").Get<string>();
    }
}
