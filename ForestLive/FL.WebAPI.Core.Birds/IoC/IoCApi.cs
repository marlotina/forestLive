using FL.DependencyInjection.Standard.Contracts;
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

namespace FL.WebAPI.Core.Birds.IoC
{
    public class IoCApi: IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IBirdSpeciePostMapper, BirdSpeciePostMapper>();

            services.AddSingleton<IBirdsConfiguration, BirdsConfiguration>();

            services.AddTransient<ISpeciesService, SpeciesService>();
            services.AddTransient<ISearchMapService, SearchMapService>();

            services.AddTransient<IUserVotesRestRepository, UserVotesRestRepository>();
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddTransient<ISearchMapRepository, SearchMapRepository>();

            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
