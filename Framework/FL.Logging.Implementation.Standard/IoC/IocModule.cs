using FL.DependencyInjection.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;

namespace FL.Logging.Implementation.Standard.IoC
{
    public class IocModule : IModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
