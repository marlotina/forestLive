using FL.Functions.UserPost.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FL.Functions.UserPost.Startup))]
namespace FL.Functions.UserPost
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var databaseName = config["CosmosDatabaseId"];
            var account = config["CosmosdbConnection"];
            var key = config["CosmosKey"];

            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(account, key);
            CosmosClient client = clientBuilder
                .WithApplicationName(databaseName)
                .WithConnectionModeDirect()
                .WithSerializerOptions(new CosmosSerializationOptions() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
                .Build();

            var postCosmosDbService = new UserPostCosmosService(client, databaseName);

            builder.Services.AddSingleton<IUserPostCosmosService>(postCosmosDbService);
        }
    }
}