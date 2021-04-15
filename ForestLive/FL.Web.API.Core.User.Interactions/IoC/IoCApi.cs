using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Implementations;
using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Contracts;
using FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Implementation;
using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.User.Interactions.Application.Services.Implementations;
using FL.Web.API.Core.User.Interactions.Configuration.Contracts;
using FL.Web.API.Core.User.Interactions.Configuration.Implementations;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using FL.Web.API.Core.User.Interactions.Infrastructure.Repositories;
using FL.Web.API.Core.User.Interactions.Mapper.v1.Contracts;
using FL.Web.API.Core.User.Interactions.Mapper.v1.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace FL.Web.API.Core.User.Interactions.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IVoteMapper, VoteMapper>();
            services.AddSingleton<IFollowMapper, FollowMapper>();
            services.AddSingleton<ICommentMapper, CommentMapper>();

            services.AddSingleton<IVoteConfiguration, VoteConfiguration>();
            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>();

            
            services.AddTransient<IVotePostService, VotePostService>();
            services.AddTransient<IVoteCommentService, VoteCommentService>(); 
            services.AddTransient<ICommentService, CommentService>();

            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<IVotePostRepository, VotePostRepository>();
            services.AddSingleton<IVoteCommentRepository, VoteCommentRepository>();
            services.AddSingleton<IFollowRepository, FollowRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
