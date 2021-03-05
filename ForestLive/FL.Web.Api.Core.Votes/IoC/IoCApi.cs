﻿using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.Web.Api.Core.Votes.Api.Mapper.v1.Contracts;
using FL.Web.Api.Core.Votes.Api.Mapper.v1.Implementation;
using FL.Web.Api.Core.Votes.Configuration.Contracts;
using FL.Web.Api.Core.Votes.Configuration.Implementations;
using FL.Web.Api.Core.Votes.Infrastructure.CosmosDb.Contracts;
using FL.Web.Api.Core.Votes.Infrastructure.CosmosDb.Implementations;
using FL.Web.Api.Core.Votes.Infrastructure.ServiceBus.Contracts;
using FL.Web.Api.Core.Votes.Infrastructure.ServiceBus.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace FL.Web.Api.Core.Votes.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IVoteMapper, VoteMapper>();

            services.AddSingleton<IPostConfiguration, PostConfiguration>();
            //services.AddSingleton<IAzureStorageConfiguration, AzureStorageConfiguration>();

            services.AddTransient(typeof(IServiceBusVotePostTopicSender<>), typeof(ServiceBusVotePostTopicSender<>));
          
                        
            services.AddSingleton<IClientFactory, ClientFactory>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
