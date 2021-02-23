using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Implementations;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Implementations;
using FL.WebAPI.Core.Birds.Configuration.Contracts;
using FL.WebAPI.Core.Birds.Configuration.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace FL.WebAPI.Core.Birds.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services) 
        {
            services.AddSingleton<IAutocompleteMapper, AutocompleteMapper>();
            //services.AddSingleton<IUserPageMapper, UserPageMapper>();
            //services.AddSingleton<IRegisterMapper, RegisterMapper>();

            //services.AddSingleton<IUserConfiguration, UserConfiguration>();
            services.AddSingleton<IBirdsConfiguration, BirdsConfiguration>();

            //services.AddTransient<IUserService, UserService>();
            //services.AddTransient<IUserImageService, UserImageService>();
            services.AddTransient<IAutocompleteService, AutocompleteService>();

            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IUserImageRepository, UserImageRepository>();

            //services.AddSingleton<IDataBaseFactory, DataBaseFactory>();


            ////loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
