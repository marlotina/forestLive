using FL.ServiceBus.Standard.Configuration.Contracts;
using FL.ServiceBus.Standard.Configuration.Model;
using Microsoft.Extensions.Configuration;

namespace FL.ServiceBus.Standard.Configuration.Implementations
{
    public class ServiceBusConfiguration : IServiceBusConfiguration
    {
        private readonly IConfiguration configuration;

        public ServiceBusConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ServiceBusConfig ServiceBusConfig => this.configuration.GetSection("ServiceBusConfig").Get<ServiceBusConfig>();
    }
}
