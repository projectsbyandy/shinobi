using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Serilog;
using Shinobi.Core.Models;

namespace Shinobi.FunctionApp;

public class ShinobiApi
{
    private readonly ILogger _logger;

    public ShinobiApi(ILogger logger)
    {
        _logger = logger;
    }

    [Function("Status")]
    [OpenApiSecurity("x-functions-key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    public IActionResult GetShinobiApiStatus([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
    {
        _logger.Information("Shinobi Service is running");
        return new OkObjectResult("All is well");
    }
    
    [Function("Ninjas")]
    [OpenApiSecurity("x-functions-key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    public IActionResult GetAllNinjas([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
    {
        return new OkObjectResult(new List<Ninja>()
        {
            new("Lars", "Grande"),
            new("Cecile", "Perch"),
            new("Emma", "Lone")
        });
    }

    [Function("CreateNinja")]
    [OpenApiSecurity("x-functions-key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Ninja))]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Ninja), Description = "Create new Ninja")]
    public async Task<HttpResponseData> CreateNinja([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
    {
        var response = req.CreateResponse();
        var readStream = new StreamReader(req.Body);
        var ninja = JsonSerializer.Deserialize<Ninja>(await readStream.ReadToEndAsync());
        
        await response.WriteAsJsonAsync(ninja, HttpStatusCode.Created);
        return response;
    }

}