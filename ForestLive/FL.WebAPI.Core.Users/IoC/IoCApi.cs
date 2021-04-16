using Microsoft.Extensions.DependencyInjection;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Application.Services.Implementations;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Configuration.Implementations;
using FL.WebAPI.Core.Users.Domain.Repositories;
using FL.WebAPI.Core.Users.Infrastructure.AzureStorage;
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
using FL.Cache.Standard.Contracts;
using FL.Cache.Standard.Implementations;
using FL.WebAPI.Core.Items.Infrastructure.Repositories;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Implementations;

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
            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserManagedService, UserManagedService>();
            services.AddTransient<IUserImageService, UserImageService>();
            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IUserImageRepository, UserImageRepository>();
            services.AddTransient<IUserCosmosRepository, UserCosmosRepository>();

            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<IDataBaseFactory, DataBaseFactory>();

            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();
            services.AddSingleton(typeof(ICustomMemoryCache<>), typeof(CustomMemoryCache<>));


            
            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));

            //mailing
            services.AddTransient<IMailConfiguration, MailConfiguration>();
            services.AddTransient<IEmailAccountService, EmailAccountService>();
        }
    }
}
