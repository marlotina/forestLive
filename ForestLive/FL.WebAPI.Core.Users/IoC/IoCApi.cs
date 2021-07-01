using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Application.Services.Implementations;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Configuration.Implementations;
using FL.WebAPI.Core.Users.Infrastructure.Services.Contracts;
using FL.WebAPI.Core.Users.Infrastructure.Services.Implementations;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Mappers.v1.Implementation;
using FL.WebAPI.Core.Users.Api.Mappers.v1.Implementation;
using FL.WebAPI.Core.Users.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.Repositories;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.DependencyInjection.Standard.Contracts;

namespace FL.WebAPI.Core.Users.IoC
{
    public class IoCApi : IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IUserMapper, UserMapper>();
            services.AddSingleton<IUserPageMapper, UserPageMapper>();
            services.AddSingleton<IRegisterMapper, RegisterMapper>();
            services.AddSingleton<IUserLabelMapper, UserLabelMapper>();

            services.AddSingleton<IUserConfiguration, UserConfiguration>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserManagedService, UserManagedService>();
            services.AddTransient<IUserImageService, UserImageService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserLabelService, UserLabelService>();




            services.AddTransient<IUserLabelRepository, UserLabelRepository>();
            services.AddTransient<IUserCosmosRepository, UserCosmosRepository>();
            services.AddTransient<IUserFollowRepository, UserFollowRepository>();

            services.AddSingleton<IDataBaseFactory, DataBaseFactory>();
        }
    }
}
