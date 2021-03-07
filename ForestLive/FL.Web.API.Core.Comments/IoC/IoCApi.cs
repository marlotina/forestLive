using FL.Infrastructure.Standard.Configuration.Contracts;
using FL.Infrastructure.Standard.Configuration.Implementations;
using FL.Infrastructure.Standard.Contracts;
using FL.Infrastructure.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Comments.Api.Mapper.v1.Contracts;
using FL.Web.API.Core.Comments.Api.Mapper.v1.Implementation;
using FL.Web.API.Core.Comments.Application.Services.Contracts;
using FL.Web.API.Core.Comments.Application.Services.Implementations;
using FL.Web.API.Core.Comments.Configuration.Contracts;
using FL.Web.API.Core.Comments.Configuration.Implementations;
using FL.Web.API.Core.Comments.Domain.Repositories;
using FL.Web.API.Core.Comments.Infrastructure.CosmosDb.Contracts;
using FL.Web.API.Core.Comments.Infrastructure.CosmosDb.Implementations;
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
            services.AddSingleton<IBirdPostMapper, BirdPostMapper>();
            services.AddSingleton<IBirdCommentMapper, BirdCommentMapper>();

            services.AddSingleton<IPostConfiguration, PostConfiguration>();
            services.AddSingleton<IAzureStorageConfiguration, AzureStorageConfiguration>();

            services.AddTransient(typeof(IServiceBusPostTopicSender<>), typeof(ServiceBusPostTopicSender<>));
            services.AddTransient(typeof(IServiceBusVotePostTopicSender<>), typeof(ServiceBusVotePostTopicSender<>));
            services.AddTransient(typeof(IServiceBusCommentTopicSender<>), typeof(ServiceBusCommentTopicSender<>));
            
            services.AddTransient<IBirdPostService, BirdPostService>();
            services.AddTransient<ICommentService, CommentService>();
                        
            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<IBirdPostRepository, BirdPostCosmosRepository>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();
        }
    }
}
