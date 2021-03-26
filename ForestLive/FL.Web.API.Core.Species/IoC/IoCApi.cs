using FL.Cache.Standard.Contracts;
using FL.Cache.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Species.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Species.Api.Mappers.v1.Implementations;
using FL.Web.API.Core.Species.Application.Services.Contracts;
using FL.Web.API.Core.Species.Application.Services.Implementations;
using FL.Web.API.Core.Species.Configuration.Contracts;
using FL.Web.API.Core.Species.Configuration.Implementations;
using FL.Web.API.Core.Species.Domain.Repository;
using FL.Web.API.Core.Species.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FL.Web.API.Core.Species.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services) 
        {
            services.AddSingleton<IAutocompleteMapper, AutocompleteMapper>();

            services.AddSingleton<IBirdsConfiguration, BirdsConfiguration>();

            services.AddTransient<IAutocompleteService, AutocompleteService>();

            services.AddTransient<ISpeciesRepository, SpeciesRepository>();

            services.AddSingleton(typeof(ICustomMemoryCache<>), typeof(CustomMemoryCache<>));

            ////loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
