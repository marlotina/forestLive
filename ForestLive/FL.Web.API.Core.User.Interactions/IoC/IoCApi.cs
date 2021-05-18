using FL.DependencyInjection.Standard.Contracts;
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
using FL.WebAPI.Core.User.Interactions.Infrastructure.ServiceBus.Contracts;
using FL.WebAPI.Core.User.Interactions.Infrastructure.ServiceBus.Implementations;

namespace FL.Web.API.Core.User.Interactions.IoC
{
    public class IoCApi :IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IVoteMapper, VoteMapper>();
            services.AddSingleton<IFollowMapper, FollowMapper>();
            services.AddSingleton<ICommentMapper, CommentMapper>();

            services.AddSingleton<IVoteConfiguration, VoteConfiguration>();


            services.AddTransient<IVotePostService, VotePostService>();
            services.AddTransient<IVoteCommentService, VoteCommentService>();
            services.AddTransient<IFollowService, FollowService>();
            services.AddTransient<ICommentService, CommentService>();

            services.AddSingleton<IVotePostRepository, VotePostRepository>();
            services.AddSingleton<IVoteCommentRepository, VoteCommentRepository>();
            services.AddSingleton<IFollowRepository, FollowRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();

            services.AddTransient(typeof(IServiceBusFollowerTopicSender<>), typeof(ServiceBusFollowerTopicSender<>));

        }
    }
}
