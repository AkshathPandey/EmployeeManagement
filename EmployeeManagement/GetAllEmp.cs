using EmployeeManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

public static class GetAllEmployees
{
    [FunctionName("GetAllEmployees")]
    public static async Task<HttpResponseMessage> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "employees")] HttpRequestMessage req,
        ILogger log)
    {
        var connectionString = Environment.GetEnvironmentVariable("CosmosDBConnectionString");
        log.LogInformation("got connection string");
        var client = new CosmosClient(connectionString);
        var container = client.GetContainer("employeedb", "emplist");
        var query = "SELECT * FROM c";
        var employees = new List<Employee>();
        var iterator = container.GetItemQueryIterator<Employee>(query);
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            employees.AddRange(response.ToList());
        }
        return new OkObjectResult(employees);

    }
}
