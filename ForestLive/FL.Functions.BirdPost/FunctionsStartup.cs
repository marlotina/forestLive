using FL.Functions.BirdPost.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FL.Functions.BirdPost.Startup))]
namespace FL.Functions.BirdPost
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

            var postCosmosDbService = new SpeciePostCosmosService(client, databaseName);

            builder.Services.AddSingleton<ISpeciePostCosmosService>(postCosmosDbService);
        }
    }
}