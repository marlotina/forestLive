using System;

namespace FL.DependencyInjection.Standard.Contracts
{
    public interface IServiceCollection
    {
        void AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        void AddTransient(Type serviceType, Type implementationType);

        void AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        void AddSingleton(Type serviceType, Type implementationType);

        void AddScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;
    }
}
