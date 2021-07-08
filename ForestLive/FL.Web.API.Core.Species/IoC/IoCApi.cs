using FL.DependencyInjection.Standard.Contracts;
using FL.Web.API.Core.Species.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Species.Api.Mappers.v1.Implementations;
using FL.Web.API.Core.Species.Application.Services.Contracts;
using FL.Web.API.Core.Species.Application.Services.Implementations;
using FL.Web.API.Core.Species.Configuration.Contracts;
using FL.Web.API.Core.Species.Configuration.Implementations;
using FL.Web.API.Core.Species.Domain.Repository;
using FL.Web.API.Core.Species.Infrastructure.Repositories;

namespace FL.Web.API.Core.Species.IoC
{
    public class IoCApi : IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IAutocompleteMapper, AutocompleteMapper>();

            services.AddSingleton<IBirdsConfiguration, BirdsConfiguration>();

            services.AddTransient<IAutocompleteSpeciesService, AutocompleteSpeciesService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<ISpeciesService, SpeciesService>();
            
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddTransient<ICountriesRepository, CountriesRepository>();
        }
    }
}
