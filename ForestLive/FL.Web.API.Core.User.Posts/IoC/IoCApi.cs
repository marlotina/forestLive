using FL.CosmosDb.Standard.Configuration.Contracts;
using FL.CosmosDb.Standard.Configuration.Implementations;
using FL.CosmosDb.Standard.Contracts;
using FL.CosmosDb.Standard.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.User.Posts.Application.Services.Contracts;
using FL.WebAPI.Core.User.Posts.Application.Services.Implementations;
using FL.WebAPI.Core.User.Posts.Configuration.Contracts;
using FL.WebAPI.Core.User.Posts.Configuration.Implementations;
using FL.WebAPI.Core.User.Posts.Domain.Repositories;
using FL.WebAPI.Core.User.Posts.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FL.WebAPI.Core.User.Posts.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IBirdPostMapper, BirdPostMapper>();

            services.AddSingleton<IUserPostConfiguration, UserPostConfiguration>();
            services.AddSingleton<ICosmosConfiguration, CosmosConfiguration>(); 


            services.AddTransient<IUserPostService, UserPostService>();
                        
            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddTransient<IBirdUserRepository, BirdUserCosmosRepository>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}
