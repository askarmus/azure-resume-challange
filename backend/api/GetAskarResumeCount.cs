using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

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
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "azureresume", collectionName: "counter", ConnectionStringSetting = "CosmosConnectionString", Id = "1", PartitionKey ="1")] Counter counter,
            [CosmosDB(databaseName: "azureresume", collectionName: "counter", ConnectionStringSetting = "CosmosConnectionString", Id = "1", PartitionKey ="1")] out Counter updatedCounter,
            ILogger log)
        {

           updatedCounter = counter;
           updatedCounter.id = 1;
           updatedCounter.Count = 20;

            log.LogInformation("C# HTTP trigger function processed a request.");

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(counter), System.Text.Encoding.UTF8, "application/json")
            };
        }
    }
}
