using FL.Infrastructure.Implementations.Domain.Repository;
using FL.Infrastructure.Implementations.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Configuration.Implementations;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.Repositories;
using FL.WebAPI.Core.Items.Services.Contracts;
using FL.WebAPI.Core.Items.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace FL.WebAPI.Core.Items.IoC
{
    public static class IoCApi
    {
        public static void AddInjection(IServiceCollection services)
        {
            services.AddSingleton<IBirdPostMapper, BirdPostMapper>();

            services.AddSingleton<IItemConfiguration, ItemConfiguration>();

            services.AddTransient<IBirdPostService, BirdPostService>();

            services.AddTransient<IPostRepository, PostRepository>();


            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));

            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();
        }
    }
}
