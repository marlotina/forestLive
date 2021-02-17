using FL.Infrastructure.Standard.Configuration.Contracts;
using FL.Infrastructure.Standard.Configuration.Implementations;
using FL.Infrastructure.Standard.Contracts;
using FL.Infrastructure.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.ServiceBus.Standard.Configuration.Contracts;
using FL.ServiceBus.Standard.Configuration.Implementations;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Application.Services.Implementations;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Configuration.Implementations;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Implementations;
using FL.WebAPI.Core.Items.Infrastructure.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Implementations;
using FL.WebAPI.Core.Items.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Mapper.v1.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace FL.WebAPI.Core.Items.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IBirdPostMapper, BirdPostMapper>();
            services.AddSingleton<IBirdCommentMapper, BirdCommentMapper>();

            services.AddSingleton<IItemConfiguration, ItemConfiguration>();
            services.AddSingleton<IAzureStorageConfiguration, AzureStorageConfiguration>();

            services.AddTransient<IServiceBusConfiguration, ServiceBusConfiguration>();
            services.AddTransient(typeof(IServiceBusCreatedPostTopicSender<>), typeof(ServiceBusCreatedPostTopicSender<>));
            //services.AddTransient(typeof(IServiceBusDeletedPostTopicSender<>), typeof(ServiceBusDeletedPostTopicSender<>));

            services.AddTransient<IBirdPostService, BirdPostService>();
            services.AddTransient<IBirdUserPostService, BirdUserPostService>();
            services.AddTransient<IBirdCommentService, BirdCommentService>();
                        
            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<IBirdUserRepository, BirdUserCosmosRepository>();
            services.AddSingleton<IBirdPostRepository, BirdPostCosmosRepository>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();
        }
    }
}
