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
using FL.Mailing.Contracts.Standard;
using FL.Sendgrid.Implementation.Standard.Configuration.Contracts;
using FL.Sendgrid.Implementation.Standard.Implementations;
using FL.Sendgrid.Implementation.Standard.Configuration.Implementations;
using FL.Infrastructure.Standard.Configuration.Contracts;
using FL.Infrastructure.Standard.Configuration.Implementations;
using FL.Infrastructure.Standard.Contracts;
using FL.Infrastructure.Standard.Implementations;

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
            services.AddSingleton<IAzureStorageConfiguration, AzureStorageConfiguration>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserImageService, UserImageService>();
            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserImageRepository, UserImageRepository>();

            services.AddSingleton<IDataBaseFactory, DataBaseFactory>();

            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));

            //mailing
            services.AddTransient<IMailConfiguration, MailConfiguration>();
            services.AddTransient<IEmailAccountService, EmailAccountService>();
        }
    }
}
