using FL.DependencyInjection.Standard.Contracts;
using FL.WebAPI.Core.UserLabels.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.UserLabels.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.UserLabels.Application.Services.Contracts;
using FL.WebAPI.Core.UserLabels.Application.Services.Implementations;
using FL.WebAPI.Core.UserLabels.Configuration.Contracts;
using FL.WebAPI.Core.UserLabels.Configuration.Implementations;
using FL.WebAPI.Core.UserLabels.Domain.Repositories;
using FL.WebAPI.Core.UserLabels.Infrastructure.Repositories;

namespace FL.WebAPI.Core.UserLabels.IoC
{
    public class IoCApi : IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IBirdPostMapper, BirdPostMapper>();

            services.AddSingleton<IUserLabelConfiguration, UserLabelConfiguration>();

            services.AddTransient<IUserLabelService, UserLabelService>();

            services.AddTransient<IUserLabelRepository, UserLabelRepository>();
        }
    }
}
