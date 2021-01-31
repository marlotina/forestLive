using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Drawing;

namespace Functions.BirdPhoto
{
    public static class AddPhotoFunction
    {
        [FunctionName("AddBirdPhoto")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)
            ] HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var input = JsonConvert.DeserializeObject<Product>(requestBody);

            var product = new Product
            {
                ProductName = input.ProductName,
                ProductType = input.ProductType,
                Price = input.Price,
                Manufacturer = input.Manufacturer
            };

            await products.AddAsync(product);

            return new OkObjectResult(product);
        }
    }
}
