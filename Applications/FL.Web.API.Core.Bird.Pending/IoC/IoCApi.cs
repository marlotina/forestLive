using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.DependencyInjection.Standard.Contracts;
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

namespace FL.Web.API.Core.Bird.Pending.IoC
{
    public class IoCApi : IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IBirdPendingMapper, BirdPendingMapper>();

            services.AddSingleton<IBirdPendingConfiguration, BirdPendingConfiguration>();

            services.AddTransient<IBirdPendingService, BirdPendingService>();

            services.AddTransient<IUserVotesRestRepository, UserVotesRestRepository>();

            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddTransient<IBirdPendingRepository, BirdPendingRepository>();

            ////loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
