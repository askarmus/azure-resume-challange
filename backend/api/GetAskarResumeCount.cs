using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using System.Diagnostics.Metrics;
using azure_resume_challange;

namespace Company.Function
{



    public static class GetAskarResumeCount
    {
 
        private const string DATABASE_NAME = "azureresume";
        private const string COLLECTION_NAME = "counter";

        [FunctionName("GetAskarResumeCount")]
        public static async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
           [CosmosDB(DATABASE_NAME, COLLECTION_NAME, Id = "id", ConnectionStringSetting = "CosmosConnectionString")] IAsyncCollector<Counter> counterItemsOut,
           [CosmosDB(DATABASE_NAME, COLLECTION_NAME, ConnectionStringSetting = "CosmosConnectionString")] DocumentClient client,
           ILogger log)
        {
            log.LogInformation("Arrived counter request");

 
            await counterItemsOut.AddAsync(new Counter()
            {
                Id = DateTime.Now.Ticks.ToString(),
                Date = DateTime.Now,

            });

            var collectionUri = UriFactory.CreateDocumentCollectionUri(DATABASE_NAME, COLLECTION_NAME);
            var query = client.CreateDocumentQuery<dynamic>(collectionUri, new SqlQuerySpec()
            {
                QueryText = "SELECT VALUE COUNT(1) FROM Counters",
            });

            var stats = new Stats();
            foreach (dynamic res in query)
            {
                stats.TotalCount = res;
            }
            return new OkObjectResult(stats);

        }
    }

    public class Stats
    {
        public int TotalCount { get; set; }
    }
}
