using FL.DependencyInjection.Standard.Contracts;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.ServiceBus.Standard.Contracts;
using FL.ServiceBus.Standard.Implementations;
using FL.WebAPI.Core.Birds.Infrastructure.Repositories;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Application.Services.Implementations;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Configuration.Implementations;
using FL.WebAPI.Core.Items.Domain.Repository;

namespace FL.WebAPI.Core.Items.IoC
{
    public class IoCApi : IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IPostMapper, PostMapper>();

            services.AddSingleton<IPostConfiguration, PostConfiguration>();

            services.AddTransient(typeof(IServiceBusTopicSender<>), typeof(ServiceBusTopicSender<>));

            services.AddTransient<IManagePostService, ManagePostService>();

            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddTransient<IUserPostRepository, UserPostRepository>();
            
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
