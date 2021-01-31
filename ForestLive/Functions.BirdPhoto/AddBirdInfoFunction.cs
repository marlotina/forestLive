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
using static System.Net.Mime.MediaTypeNames;

namespace Functions.BirdPhoto
{
    public static class FunctionAddPhoto
    {
        [FunctionName("AddPhoto")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
            CloudBlobClient client = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference("images");

            var blob = container.GetBlockBlobReference(blobName);
            await blob.UploadFromByteArrayAsync(data, 0, data.Length);

            return true;
        }

    }
}
