using FL.DependencyInjection.Standard.Contracts;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Birds.Infrastructure.Repositories;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Application.Services.Implementations;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Configuration.Implementations;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Domain.Repository;
using FL.WebAPI.Core.Items.Infrastructure.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Implementations;

namespace FL.WebAPI.Core.Items.IoC
{
    public class IoCApi : IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IPostMapper, PostMapper>();

            services.AddSingleton<IPostConfiguration, PostConfiguration>();

            services.AddTransient(typeof(IServiceBusLabelTopicSender<>), typeof(ServiceBusLabelTopicSender<>));
            services.AddTransient(typeof(IServiceBusAssignSpecieTopicSender<>), typeof(ServiceBusAssignSpecieTopicSender<>));

            services.AddTransient<IManagePostService, ManagePostService>();

            services.AddTransient<IPostRepository, PostCosmosRepository>();
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddTransient<IUserPostRepository, UserPostRepository>();
            
            services.AddTransient<IUserVotesRepository, UserVotesRepository>();

            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
