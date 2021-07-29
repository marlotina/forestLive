using FL.DependencyInjection.Standard.Contracts;
using FL.BlobContainer.Standard.Configuration.Contracts;
using FL.BlobContainer.Standard.Configuration.Implementations;
using FL.BlobContainer.Standard.Contracts;
using FL.BlobContainer.Standard.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace FL.BlobContainer.Standard.IoC
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
