using FL.DependencyInjection.Standard.Contracts;
using FL.Web.API.Core.ExternalData.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.ExternalData.Api.Mappers.v1.Implementations;
using FL.Web.API.Core.ExternalData.Application.Mappers.Contracts;
using FL.Web.API.Core.ExternalData.Application.Mappers.Implementations;
using FL.Web.API.Core.ExternalData.Application.Services.Contracts;
using FL.Web.API.Core.ExternalData.Application.Services.Implementations;
using FL.Web.API.Core.ExternalData.Configuration.Contracts;
using FL.Web.API.Core.ExternalData.Configuration.Implementations;
using FL.Web.API.Core.ExternalData.Domain.Repository;
using FL.Web.API.Core.ExternalData.Infrastructure.Repositories;

namespace FL.Web.API.Core.ExternalData.IoC
{
    public class IoCApi : IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IAutocompleteMapper, AutocompleteMapper>();
            services.AddSingleton<ISpecieMapper, SpecieMapper>();
            services.AddSingleton<ISpecieCacheMapper, SpecieCacheMapper>();

            services.AddSingleton<IBirdsConfiguration, BirdsConfiguration>();

            services.AddTransient<IAutocompleteService, AutocompleteService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<ISpeciesService, SpeciesService>();
            
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddTransient<ICountriesRepository, CountriesRepository>();
        }
    }
}
