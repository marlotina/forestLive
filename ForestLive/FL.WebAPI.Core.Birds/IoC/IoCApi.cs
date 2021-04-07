using FL.Cache.Standard.Contracts;
using FL.Cache.Standard.Implementations;
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
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Implementations;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Implementations;
using FL.WebAPI.Core.Birds.Configuration.Contracts;
using FL.WebAPI.Core.Birds.Configuration.Implementations;
using FL.WebAPI.Core.Birds.Domain.Repository;
using FL.WebAPI.Core.Birds.Infrastructure.Repositories;
using FL.WebAPI.Core.Birds.Infrastructure.ServiceBus.Contracts;
using FL.WebAPI.Core.Birds.Infrastructure.ServiceBus.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace FL.WebAPI.Core.Birds.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services) 
        {
            services.AddSingleton<IBirdSpeciePostMapper, BirdSpeciePostMapper>();

            services.AddSingleton<IAzureStorageConfiguration, AzureStorageConfiguration>();
            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>();
            services.AddSingleton<IBirdsConfiguration, BirdsConfiguration>();

            services.AddTransient<IBirdSpeciesService, BirdSpeciesService>();
            services.AddTransient<ISearchMapService, SearchMapService>();
            services.AddTransient<IManagePostSpeciesService, ManagePostSpeciesService>();

            services.AddTransient<IUserVotesRestRepository, UserVotesRestRepository>();

            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddTransient<IBirdSpeciesRepository, BirdSpeciesRepository>();
            services.AddTransient<ISearchMapRepository, SearchMapRepository>();

            services.AddTransient(typeof(IServiceBusPostTopicSender<>), typeof(ServiceBusPostTopicSender<>));
            services.AddTransient(typeof(IServiceBusLabelTopicSender<>), typeof(ServiceBusLabelTopicSender<>));
            services.AddTransient(typeof(IServiceBusAssignSpecieTopicSender<>), typeof(ServiceBusAssignSpecieTopicSender<>));
            

            services.AddSingleton(typeof(ICustomMemoryCache<>), typeof(CustomMemoryCache<>));
            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();

            ////loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
