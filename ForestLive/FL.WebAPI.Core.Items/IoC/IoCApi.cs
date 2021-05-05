using FL.DependencyInjection.Standard.Contracts;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Application.Services.Implementations;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Configuration.Implementations;
using FL.WebAPI.Core.Items.Domain.Repositories;
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

            services.AddTransient(typeof(IServiceBusPostTopicSender<>), typeof(ServiceBusPostTopicSender<>));
            services.AddTransient(typeof(IServiceBusLabelTopicSender<>), typeof(ServiceBusLabelTopicSender<>));

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IManagePostService, ManagePostService>();

            services.AddTransient<IPostRepository, PostCosmosRepository>();
            services.AddTransient<IUserVotesRepository, UserVotesRepository>();
        }
    }
}
