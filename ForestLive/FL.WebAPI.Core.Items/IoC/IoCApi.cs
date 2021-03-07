using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Implementations;
using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.Infrastructure.Standard.Configuration.Contracts;
using FL.Infrastructure.Standard.Configuration.Implementations;
using FL.Infrastructure.Standard.Contracts;
using FL.Infrastructure.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Application.Services.Implementations;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Configuration.Implementations;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace FL.WebAPI.Core.Items.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IPostMapper, PostMapper>();

            services.AddSingleton<IPostConfiguration, PostConfiguration>();
            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>();
            
            services.AddSingleton<IAzureStorageConfiguration, AzureStorageConfiguration>();

            services.AddTransient(typeof(IServiceBusPostTopicSender<>), typeof(ServiceBusPostTopicSender<>));
            
            services.AddTransient<IPostService, PostService>();
            
            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<IPostRepository, PostCosmosRepository>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();
        }
    }
}
