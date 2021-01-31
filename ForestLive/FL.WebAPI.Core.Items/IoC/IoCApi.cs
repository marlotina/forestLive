using FL.Infrastructure.Implementations.Domain.Repository;
using FL.Infrastructure.Implementations.Implementations;
using FL.Infrastructure.Standard.Configuration.Contracts;
using FL.Infrastructure.Standard.Configuration.Implementations;
using FL.Logging.Implementation.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Configuration.Implementations;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.CosmosDb.Implementations;
using FL.WebAPI.Core.Items.Infrastructure.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.Services.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.Services.Implementations;
using FL.WebAPI.Core.Items.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Mapper.v1.Implementation;
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
            services.AddSingleton<ICommentMapper, CommentMapper>();

            services.AddSingleton<IItemConfiguration, ItemConfiguration>();
            services.AddSingleton<IAzureStorageConfiguration, AzureStorageConfiguration>();

            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<ICommentService, CommentService>();

            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();
            
            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<IUserCosmosRepository, UserCosmosRepository>();
            services.AddSingleton<IItemsCosmosRepository, ItemsCosmosRepository>();

            //loggin
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient<IBlobContainerRepository, BlobContainerRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
        }
    }
}
