using FL.DependencyInjection.Standard.Contracts;
using FL.ServiceBus.Standard.Configuration.Contracts;
using FL.ServiceBus.Standard.Configuration.Implementations;
using FL.ServiceBus.Standard.Contracts;
using FL.ServiceBus.Standard.Implementations;

namespace FL.ServiceBus.Standard.IoC
{
    public class IocModule : IModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IServiceBusTopicSender<>), typeof(ServiceBusTopicSender<>));
            services.AddSingleton<IServiceBusConfiguration, ServiceBusConfiguration>();
        }
    }
}
