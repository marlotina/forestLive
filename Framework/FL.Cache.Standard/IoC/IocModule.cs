using FL.Cache.Standard.Contracts;
using FL.Cache.Standard.Implementations;
using FL.DependencyInjection.Standard.Contracts;

namespace FL.Cache.Standard.IoC
{
    public class IocModule : IModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(ICustomMemoryCache<>), typeof(CustomMemoryCache<>));
        }
    }
}
