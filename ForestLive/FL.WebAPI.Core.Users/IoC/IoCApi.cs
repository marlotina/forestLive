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

namespace FL.WebAPI.Core.Users.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddTransient<IUserMapper, UserMapper>();
            services.AddTransient<IRegisterMapper, RegisterMapper>();

            services.AddTransient<IUserConfiguration, UserConfiguration>();
            services.AddTransient<IUserImageRepository, UserImageRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserImageService, UserImageService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IEmailAccountService, EmailAccountService>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddSingleton<IDataBaseFactory, DataBaseFactory>();
        }
    }
}
