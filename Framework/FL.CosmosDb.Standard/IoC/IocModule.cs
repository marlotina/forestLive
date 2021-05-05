using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Implementations;
using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.DependencyInjection.Standard.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FL.CosmosDb.Standard.IoC
{
    public class IocModule : IModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>();
            services.AddSingleton<IClientFactory, ClientFactory>();
        }
    }
}
