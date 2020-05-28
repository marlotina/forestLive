using Microsoft.Extensions.DependencyInjection;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Application.Services.Implementations;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Configuration.Implementations;
using FL.WebAPI.Core.Users.Domain.Repositories;
using FL.WebAPI.Core.Users.Infrastructure.AzureStorage;
using FL.WebAPI.Core.Users.Infrastructure.Repositories;
using FL.WebAPI.Core.Users.Infrastructure.Services.Contracts;
using FL.WebAPI.Core.Users.Infrastructure.Services.Implementations;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Mappers.v1.Implementation;
using FL.WebAPI.Core.Users.Api.Mappers.v1.Implementation;
using FL.WebAPI.Core.Users.Api.Mappers.v1.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Logging.Implementation.Standard;

namespace FL.WebAPI.Core.Users.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IUserMapper, UserMapper>();
            services.AddSingleton<IUserPageMapper, UserPageMapper>();
            services.AddSingleton<IRegisterMapper, RegisterMapper>();

            services.AddSingleton<IUserConfiguration, UserConfiguration>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserImageService, UserImageService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IEmailAccountService, EmailAccountService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserImageRepository, UserImageRepository>();

            services.AddSingleton<IDataBaseFactory, DataBaseFactory>();

            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
