using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FL.Functions.BirdPost
{
    public static class UserAddPost
    {
        [FunctionName("UserAddPost")]
        public static void Run([ServiceBusTrigger(
            "posts",
            "user",
            Connection = "ServiceBusConnectionString")] string mySbMsg, 
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
