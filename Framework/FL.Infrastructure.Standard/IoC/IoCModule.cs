using FL.DependencyInjection.Standard.Contracts;
using FL.Infrastructure.Standard.Configuration.Contracts;
using FL.Infrastructure.Standard.Configuration.Implementations;
using FL.Infrastructure.Standard.Contracts;
using FL.Infrastructure.Standard.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace FL.Infrastructure.Standard.IoC
{
    public class IoCModule : IModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IAzureStorageConfiguration, AzureStorageConfiguration>();
            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();
        }
    }
}
