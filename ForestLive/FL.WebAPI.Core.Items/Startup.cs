using FL.WebAPI.Core.Items.Configuration.Models;
using FL.WebAPI.Core.Items.Infrastructure.Services.Contracts;
using FL.WebAPI.Core.Items.Infrastructure.Services.Implementations;
using FL.WebAPI.Core.Items.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IoCApi.AddInjection(services);

            
            services.AddCors();
            services.AddControllers();
            services.AddSingleton<IBlogCosmosDbService>(InitializeCosmosBlogClientInstanceAsync(Configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>()));

            //services.AddSingleton<IBlogCosmosDbService>(InitializeCosmosBlogClientInstanceAsync(Configuration.GetSection("CosmosDbBlog")).GetAwaiter().GetResult());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static BlogCosmosDbService InitializeCosmosBlogClientInstanceAsync(CosmosConfiguration configurationSection)
        {
            string databaseName = configurationSection.CosmosDatabaseId;
            string account = configurationSection.CosmosdbConnection;
            string key = configurationSection.CosmosKey;

            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(account, key);
            CosmosClient client = clientBuilder
                .WithApplicationName(databaseName)
                .WithApplicationName(Regions.EastUS)
                .WithConnectionModeDirect()
                .WithSerializerOptions(new CosmosSerializationOptions() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
                .Build();
            var blogCosmosDbService = new BlogCosmosDbService(client, databaseName);
            DatabaseResponse database = client.CreateDatabaseIfNotExistsAsync(databaseName).Result;

            //IMPORTANT: Container name is also specified in the BlogCosmosDbService
            //var itemContainer = configurationSection.GetSection("CosmosBirdContainer").Value;
            //var userContainer = configurationSection.GetSection("CosmosUserContainer").Value;
            database.Database.CreateContainerIfNotExistsAsync(configurationSection.CosmosUserContainer, "/userId", 400);
            database.Database.CreateContainerIfNotExistsAsync(configurationSection.CosmosBirdContainer, "/itemId", 400);

            //await UpsertStoredProcedureAsync(postsContainer, @"CosmosDbScripts\sprocs\createComment.js");
            //await UpsertStoredProcedureAsync(postsContainer, @"CosmosDbScripts\sprocs\createLike.js");
            //await UpsertStoredProcedureAsync(postsContainer, @"CosmosDbScripts\sprocs\deleteLike.js");
            //await UpsertStoredProcedureAsync(postsContainer, @"CosmosDbScripts\sprocs\updateUsernames.js");


            //add the feed container post-trigger (for truncated the number of items in the Feed container).
            //var feedContainer = database.Database.GetContainer("Feed");
            //await UpsertTriggerAsync(feedContainer, @"CosmosDbScripts\triggers\truncateFeed.js", TriggerOperation.All, TriggerType.Post);

            return blogCosmosDbService;
        }
    }
}
