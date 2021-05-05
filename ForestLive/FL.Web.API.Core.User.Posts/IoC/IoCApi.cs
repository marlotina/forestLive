using FL.DependencyInjection.Standard.Contracts;
using FL.Web.API.Core.User.Posts.Domain.Repositories;
using FL.Web.API.Core.User.Posts.Infrastructure.Repositories;
using FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.User.Posts.Application.Services.Contracts;
using FL.WebAPI.Core.User.Posts.Application.Services.Implementations;
using FL.WebAPI.Core.User.Posts.Configuration.Contracts;
using FL.WebAPI.Core.User.Posts.Configuration.Implementations;
using FL.WebAPI.Core.User.Posts.Domain.Repositories;
using FL.WebAPI.Core.User.Posts.Infrastructure.Repositories;

namespace FL.WebAPI.Core.User.Posts.IoC
{
    public class IoCApi : IModule
    {
        public void RegisterServices(DependencyInjection.Standard.Contracts.IServiceCollection services)
        {
            services.AddSingleton<IBirdPostMapper, BirdPostMapper>();

            services.AddSingleton<IUserPostConfiguration, UserPostConfiguration>();

            services.AddTransient<IUserPostService, UserPostService>();
            services.AddTransient<IUserVoteService, UserVoteService>();

            services.AddTransient<IBirdUserRepository, BirdUserCosmosRepository>();
            services.AddTransient<IUserVotesRestRepository, UserVotesRestRepository>();
        }
    }
}
