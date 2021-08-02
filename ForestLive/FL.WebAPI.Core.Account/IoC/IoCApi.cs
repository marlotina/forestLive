using FL.WebAPI.Core.Account.Application.Services.Contracts;
using FL.WebAPI.Core.Account.Application.Services.Implementations;
using FL.WebAPI.Core.Account.Configuration.Contracts;
using FL.WebAPI.Core.Account.Configuration.Implementations;
using FL.WebAPI.Core.Account.Infrastructure.Services.Contracts;
using FL.WebAPI.Core.Account.Infrastructure.Services.Implementations;
using FL.WebAPI.Core.Account.Mappers.v1.Contracts;
using FL.WebAPI.Core.Account.Mappers.v1.Implementation;
using FL.WebAPI.Core.Items.Infrastructure.Repositories;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.DependencyInjection.Standard.Contracts;

namespace FL.WebAPI.Core.Account.IoC
{
    public class IoCApi : IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IUserMapper, UserMapper>();
            services.AddSingleton<IRegisterMapper, RegisterMapper>();

            services.AddSingleton<IUserConfiguration, UserConfiguration>();

            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IUserCosmosRepository, UserCosmosRepository>();

            services.AddSingleton<IDataBaseFactory, DataBaseFactory>();
        }
    }
}
