using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Implementations;
using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.UserLabels.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.UserLabels.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.UserLabels.Application.Services.Contracts;
using FL.WebAPI.Core.UserLabels.Application.Services.Implementations;
using FL.WebAPI.Core.UserLabels.Configuration.Contracts;
using FL.WebAPI.Core.UserLabels.Configuration.Implementations;
using FL.WebAPI.Core.UserLabels.Domain.Repositories;
using FL.WebAPI.Core.UserLabels.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FL.WebAPI.Core.UserLabels.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IBirdPostMapper, BirdPostMapper>();

            services.AddSingleton<IUserLabelConfiguration, UserLabelConfiguration>();
            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>();


            services.AddTransient<IUserLabelService, UserLabelService>();

            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddTransient<IUserLabelRepository, UserLabelRepository>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
