using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public class Counter
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }

    public static class GetAskarResumeCount
    {
        [FunctionName("GetAskarResumeCount")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "azureresume", containerName: "counter", Connection = "CosmosConnectionString", Id = "1", PartitionKey ="1")] Counter counter,
           // [CosmosDB(databaseName: "azureresume", containerName: "counter", Connection = "CosmosConnectionString", Id = "1", PartitionKey ="1")] out Counter updatedCounter,
            ILogger log)
        {

           // updatedCounter = counter;
           // updatedCounter.Count += 1;

            log.LogInformation("C# HTTP trigger function processed a request.");

            return new ObjectResult(new Counter() { Count = 10, id = 1 }); 
        }
    }
}
