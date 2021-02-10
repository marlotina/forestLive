using FL.ServiceBus.Standard.Configuration.Model;

namespace FL.ServiceBus.Standard.Configuration.Contracts
{
    public interface IServiceBusConfiguration
    {
        ServiceBusConfig ServiceBusConfig { get; }
    }
}
