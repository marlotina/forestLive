using Microsoft.Extensions.DependencyInjection;

namespace FL.DependencyInjection.Standard.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static Contracts.IServiceCollection AddLibraryServices(this IServiceCollection services, 
                params Contracts.IModule[] modules)
        {
            var serviceCollection = new FL.DependencyInjection.Standard.Implementations.ServiceCollection(services);

            foreach (var module in modules)
            {
                module.RegisterServices(serviceCollection);
            }

            return serviceCollection;
        }
    }
}
