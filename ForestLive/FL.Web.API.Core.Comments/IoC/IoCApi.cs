using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Comments.Application.Services.Contracts;
using FL.Web.API.Core.Comments.Application.Services.Implementations;
using FL.Web.API.Core.Comments.Configuration.Contracts;
using FL.Web.API.Core.Comments.Configuration.Implementations;
using FL.Web.API.Core.Comments.Domain.Repositories;
using FL.Web.API.Core.Comments.Infrastructure.Repositories;
using FL.Web.API.Core.Comments.Infrastructure.ServiceBus.Contracts;
using FL.Web.API.Core.Comments.Infrastructure.ServiceBus.Implementations;
using FL.Web.API.Core.Comments.Mapper.v1.Contracts;
using FL.Web.API.Core.Comments.Mapper.v1.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace FL.Web.API.Core.Comments.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IBirdCommentMapper, BirdCommentMapper>();

            services.AddSingleton<IPostConfiguration, PostConfiguration>();

            services.AddTransient(typeof(IServiceBusCommentTopicSender<>), typeof(ServiceBusCommentTopicSender<>));
            
            services.AddTransient<ICommentService, CommentService>();
                        
            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<ICommentRepository, CommentRepository>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
