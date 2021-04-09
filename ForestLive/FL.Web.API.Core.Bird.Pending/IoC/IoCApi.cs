﻿using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Implementations;
using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.Infrastructure.Standard.Configuration.Contracts;
using FL.Infrastructure.Standard.Configuration.Implementations;
using FL.Infrastructure.Standard.Contracts;
using FL.Infrastructure.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Bird.Pending.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Bird.Pending.Api.Mappers.v1.Implementations;
using FL.Web.API.Core.Bird.Pending.Application.Services.Contracts;
using FL.Web.API.Core.Bird.Pending.Application.Services.Implementations;
using FL.Web.API.Core.Bird.Pending.Configuration.Contracts;
using FL.Web.API.Core.Bird.Pending.Configuration.Implementations;
using FL.Web.API.Core.Bird.Pending.Domain.Repository;
using FL.Web.API.Core.Bird.Pending.Infrastructure.Repositories;
using FL.Web.API.Core.Bird.Pending.Infrastructure.ServiceBus.Contracts;
using FL.Web.API.Core.Bird.Pending.Infrastructure.ServiceBus.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace FL.Web.API.Core.Bird.Pending.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services) 
        {
            services.AddSingleton<IBirdPendingMapper, BirdPendingMapper>();

            services.AddSingleton<IAzureStorageConfiguration, AzureStorageConfiguration>();
            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>();
            services.AddSingleton<IBirdPendingConfiguration, BirdPendingConfiguration>();

            services.AddTransient<IBirdPendingService, BirdPendingService>();
            services.AddTransient<IManagePostSpeciesService, ManagePostSpeciesService>();

            services.AddTransient<IUserVotesRestRepository, UserVotesRestRepository>();

            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddTransient<IBirdPendingRepository, BirdPendingRepository>();

            services.AddTransient(typeof(IServiceBusPostTopicSender<>), typeof(ServiceBusPostTopicSender<>));
            services.AddTransient(typeof(IServiceBusLabelTopicSender<>), typeof(ServiceBusLabelTopicSender<>));
            services.AddTransient(typeof(IServiceBusAssignSpecieTopicSender<>), typeof(ServiceBusAssignSpecieTopicSender<>));


            
            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();

            ////loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}