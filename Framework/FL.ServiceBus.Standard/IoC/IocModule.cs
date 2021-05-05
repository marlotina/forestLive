using FL.DependencyInjection.Standard.Contracts;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;

namespace FL.ServiceBus.Standard.IoC
{
    public class IocModule : IModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
