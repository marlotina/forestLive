﻿using FL.DependencyInjection.Standard.Contracts;
using FL.ServiceBus.Standard.Contracts;
using FL.ServiceBus.Standard.Implementations;
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

namespace FL.Web.API.Core.User.Interactions.IoC
{
    public class IoCApi :IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IVoteMapper, VoteMapper>();
            services.AddSingleton<IFollowMapper, FollowMapper>();
            services.AddSingleton<ICommentMapper, CommentMapper>();

            services.AddSingleton<IUserInteractionsConfiguration, UserInteractionsConfiguration>();

            services.AddTransient<IVotePostService, VotePostService>();
            services.AddTransient<IVoteCommentService, VoteCommentService>();
            services.AddTransient<IFollowService, FollowService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IFollowerService, FollowerService>();

            services.AddSingleton<IVotePostRepository, VotePostRepository>();
            services.AddSingleton<IVoteCommentRepository, VoteCommentRepository>();
            services.AddSingleton<IFollowRepository, FollowRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();
            services.AddSingleton<IFollowerRepository, FollowerRepository>();

            services.AddTransient(typeof(IServiceBusTopicSender<>), typeof(ServiceBusTopicSender<>));

        }
    }
}
