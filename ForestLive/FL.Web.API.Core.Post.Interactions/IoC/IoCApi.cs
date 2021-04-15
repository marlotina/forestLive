using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Implementations;
using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Post.Interactions.Api.Mapper.v1.Contracts;
using FL.Web.API.Core.Post.Interactions.Api.Mapper.v1.Implementation;
using FL.Web.API.Core.Post.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.Post.Interactions.Application.Services.Implementations;
using FL.Web.API.Core.Post.Interactions.Configuration.Contracts;
using FL.Web.API.Core.Post.Interactions.Configuration.Implementations;
using FL.Web.API.Core.Post.Interactions.Domain.Repositories;
using FL.Web.API.Core.Post.Interactions.Infrastructure.Repositories;
using FL.Web.API.Core.Post.Interactions.Infrastructure.ServiceBus.Contracts;
using FL.Web.API.Core.Post.Interactions.Infrastructure.ServiceBus.Implementations;
using FL.Web.API.Core.Post.Interactions.Mapper.v1.Contracts;
using FL.Web.API.Core.Post.Interactions.Mapper.v1.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace FL.Web.Api.Core.Post.Interactions.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IVoteMapper, VoteMapper>();
            services.AddSingleton<ICommentMapper, CommentMapper>();

            services.AddSingleton<IPostConfiguration, PostConfiguration>();
            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>();
            
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IVotePostService, VotePostService>();
            services.AddTransient<IVoteCommentService, VoteCommentService>();
            services.AddTransient(typeof(IServiceBusVotePostTopicSender<>), typeof(ServiceBusVotePostTopicSender<>));
            services.AddTransient(typeof(IServiceBusCommentTopicSender<>), typeof(ServiceBusCommentTopicSender<>));
            services.AddTransient(typeof(IServiceBusVoteCommentTopicSender<>), typeof(ServiceBusVoteCommentTopicSender<>));

            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<IVotePostRepository, VotePostRepository>();
            services.AddSingleton<IVoteCommentRepository, VoteCommentRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
