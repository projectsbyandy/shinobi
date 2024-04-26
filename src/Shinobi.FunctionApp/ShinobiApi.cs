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
using Shinobi.FunctionApp.Models;
using Shinobi.FunctionApp.Services;

namespace Shinobi.FunctionApp;

public class ShinobiApi
{
    private readonly ILogger _logger;
    private readonly IShinobiService _shinobiService;

    public ShinobiApi(ILogger logger, IShinobiService shinobiService)
    {
        _logger = logger;
        _shinobiService = shinobiService;
    }

    [Function("Status")]
    [OpenApiSecurity("x-functions-key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiOperation(operationId: "Status", tags: new[] {"Get Service Status"})]
    public IActionResult GetShinobiApiStatus([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
    {
        _logger.Information("Shinobi Service is running");
        return new OkObjectResult("All is well");
    }
    
    [Function("Ninjas")]
    [OpenApiSecurity("x-functions-key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiOperation(operationId: "GetAll", tags: new[] {"Get All Ninjas"})]
    public HttpResponseData GetAllNinjas([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequestData req)
    {
        var ninjas = _shinobiService.GetAll();

        if (ninjas.Any())
            return new ShinobiApiResponse(req)
                .WithResponseCode(HttpStatusCode.OK)
                .DueTo(ninjas);

        return new ShinobiApiResponse(req)
            .WithResponseCode(HttpStatusCode.NoContent)
            .DueToMessage("No ninjas found");
    }

    [Function("CreateNinja")]
    [OpenApiSecurity("x-functions-key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiOperation(operationId: "Create", tags: new[] {"Create Ninja"})]
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