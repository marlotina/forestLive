using FL.DependencyInjection.Standard.Contracts;
using FL.ServiceBus.Standard.Contracts;
using FL.ServiceBus.Standard.Implementations;
using FL.WebAPI.Core.Posts.Infrastructure.Repositories;
using FL.WebAPI.Core.Posts.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Posts.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.Posts.Application.Services.Contracts;
using FL.WebAPI.Core.Posts.Application.Services.Implementations;
using FL.WebAPI.Core.Posts.Configuration.Contracts;
using FL.WebAPI.Core.Posts.Configuration.Implementations;
using FL.WebAPI.Core.Posts.Domain.Repository;

namespace FL.WebAPI.Core.Posts.IoC
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
        }
    }
}
