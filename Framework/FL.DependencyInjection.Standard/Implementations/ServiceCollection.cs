using Microsoft.Extensions.DependencyInjection;
using System;

namespace FL.DependencyInjection.Standard.Implementations
{
    public class ServiceCollection : Contracts.IServiceCollection
    {
        readonly IServiceCollection iServiceCollection;

        public ServiceCollection(IServiceCollection iServiceCollection)
        {
            this.iServiceCollection = iServiceCollection;
        }

        public void AddTransient<TService, TImplementation>()
             where TService : class
             where TImplementation : class, TService
        {
            this.iServiceCollection.AddTransient<TService, TImplementation>();
        }


        public void AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this.iServiceCollection.AddSingleton<TService, TImplementation>();
        }

        public void AddScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this.iServiceCollection.AddScoped<TService, TImplementation>();
        }

        public void AddTransient(Type serviceType, Type implementationType)
        {
            this.iServiceCollection.AddTransient(serviceType, implementationType);
        }
    }
}
