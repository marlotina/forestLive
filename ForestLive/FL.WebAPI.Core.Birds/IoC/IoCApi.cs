using FL.DependencyInjection.Standard.Contracts;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Implementations;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Implementations;
using FL.WebAPI.Core.Birds.Configuration.Contracts;
using FL.WebAPI.Core.Birds.Configuration.Implementations;
using FL.WebAPI.Core.Birds.Domain.Repository;
using FL.WebAPI.Core.Birds.Infrastructure.Repositories;
using FL.WebAPI.Core.Birds.Infrastructure.ServiceBus.Contracts;
using FL.WebAPI.Core.Birds.Infrastructure.ServiceBus.Implementations;

namespace FL.WebAPI.Core.Birds.IoC
{
    public class IoCApi: IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IBirdSpeciePostMapper, BirdSpeciePostMapper>();

            services.AddSingleton<IBirdsConfiguration, BirdsConfiguration>();

            services.AddTransient<IBirdSpeciesService, BirdSpeciesService>();
            services.AddTransient<ISearchMapService, SearchMapService>();
            services.AddTransient<IManagePostSpeciesService, ManagePostSpeciesService>();

            services.AddTransient<IUserVotesRestRepository, UserVotesRestRepository>();
            services.AddTransient<IBirdSpeciesRepository, BirdSpeciesRepository>();
            services.AddTransient<ISearchMapRepository, SearchMapRepository>();

            services.AddTransient(typeof(IServiceBusPostTopicSender<>), typeof(ServiceBusPostTopicSender<>));
            services.AddTransient(typeof(IServiceBusLabelTopicSender<>), typeof(ServiceBusLabelTopicSender<>));
            services.AddTransient(typeof(IServiceBusAssignSpecieTopicSender<>), typeof(ServiceBusAssignSpecieTopicSender<>));
        }
    }
}
