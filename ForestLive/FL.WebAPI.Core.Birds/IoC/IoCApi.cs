using FL.Cache.Standard.Contracts;
using FL.Cache.Standard.Implementations;
using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Implementations;
using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
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
using Microsoft.Extensions.DependencyInjection;

namespace FL.WebAPI.Core.Birds.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services) 
        {
            services.AddSingleton<IAutocompleteMapper, AutocompleteMapper>();
            services.AddSingleton<IBirdSpeciePostMapper, BirdSpeciePostMapper>();

            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>();
            services.AddSingleton<IBirdsConfiguration, BirdsConfiguration>();

            services.AddTransient<IBirdSpeciesService, BirdSpeciesService>();
            services.AddTransient<IAutocompleteService, AutocompleteService>();
            services.AddTransient<ISearchMapService, SearchMapService>();

            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddTransient<IBirdSpeciesRepository, BirdSpeciesRepository>();
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddTransient<ISearchMapRepository, SearchMapRepository>();

            services.AddSingleton(typeof(ICustomMemoryCache<>), typeof(CustomMemoryCache<>));

            ////loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
