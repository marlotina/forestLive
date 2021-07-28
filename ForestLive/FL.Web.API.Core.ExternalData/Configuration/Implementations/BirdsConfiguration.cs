using FL.Web.API.Core.ExternalData.Configuration.Contracts;
using Microsoft.Extensions.Configuration;

namespace FL.Web.API.Core.ExternalData.Configuration.Implementations
{
    public class BirdsConfiguration : IBirdsConfiguration
    {
        private readonly IConfiguration configuration;

        public BirdsConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ConnectionString => this.configuration.GetSection("ConnectionString").Get<string>();
    }
}
