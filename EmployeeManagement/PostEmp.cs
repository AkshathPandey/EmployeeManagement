using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.Azure.Cosmos;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement;


public static class PostEmp
{
    [FunctionName("PostEmp")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "postemp")] HttpRequest req,
        ILogger log)
    {
        var connectionString = Environment.GetEnvironmentVariable("CosmosDBConnectionString");
        var cosmosClient = new CosmosClient(connectionString);
        var container = cosmosClient.GetContainer("employeedb", "emplist");
        string requestBody= await new StreamReader(req.Body).ReadToEndAsync();  
        Employee data= JsonConvert.DeserializeObject<Employee>(requestBody);
        var NewEmp = new 
        { 
            id= Guid.NewGuid().ToString(),
            data.FirstName, data.LastName,
            data.Email, data.Phone,
        };
        await container.CreateItemAsync(NewEmp);
        return new OkObjectResult(NewEmp);
    }
}
