using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Implementations;
using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Pendings.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Pendings.Api.Mapper.v1.Implementations;
using FL.WebAPI.Core.Pendings.Application.Services.Contracts;
using FL.WebAPI.Core.Pendings.Application.Services.Implementations;
using FL.WebAPI.Core.Pendings.Configurations.Contracts;
using FL.WebAPI.Core.Pendings.Configurations.Implementations;
using FL.WebAPI.Core.Pendings.Domain.Repository;
using FL.WebAPI.Core.Pendings.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace FL.WebAPI.Core.Pendings.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IPendingPostMapper, PendingPostMapper>();

            services.AddSingleton<IPendingConfiguration, PendingConfiguration>();
            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>();

            services.AddTransient<IPendingPostService, PendingPostService>();

            services.AddTransient<IPendingPostRepository, PendingPostRepository>();

            services.AddSingleton<IClientFactory, ClientFactory>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
