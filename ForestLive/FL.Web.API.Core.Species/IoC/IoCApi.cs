using FL.Cache.Standard.Contracts;
using FL.Cache.Standard.Implementations;
using FL.DependencyInjection.Standard.Contracts;
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
    public class IoCApi : IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IAutocompleteMapper, AutocompleteMapper>();

            services.AddSingleton<IBirdsConfiguration, BirdsConfiguration>();

            services.AddTransient<IAutocompleteService, AutocompleteService>();

            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
        }
    }
}
