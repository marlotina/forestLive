using Microsoft.Extensions.DependencyInjection;

namespace FL.WebAPI.Core.Birds.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            //services.AddSingleton<IUserMapper, UserMapper>();
            //services.AddSingleton<IUserPageMapper, UserPageMapper>();
            //services.AddSingleton<IRegisterMapper, RegisterMapper>();

            //services.AddSingleton<IUserConfiguration, UserConfiguration>();
            //services.AddSingleton<IAzureStorageConfiguration, AzureStorageConfiguration>();

            //services.AddTransient<IUserService, UserService>();
            //services.AddTransient<IUserImageService, UserImageService>();
            //services.AddTransient<IAccountService, AccountService>();

            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IUserImageRepository, UserImageRepository>();

            //services.AddSingleton<IDataBaseFactory, DataBaseFactory>();

            //services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();

            ////loggin
            //services.AddTransient(typeof(ILogger<>), typeof(Logger<>));

            ////mailing
            //services.AddTransient<IMailConfiguration, MailConfiguration>();
            //services.AddTransient<IEmailAccountService, EmailAccountService>();
        }
    }
}
