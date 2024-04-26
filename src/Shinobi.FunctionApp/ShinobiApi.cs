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
        _logger.Information("Get All Ninjas request has been received");

        var ninjaResponse = _shinobiService.GetAll();
        
        return new ShinobiApiResponse(req)
                .WithResponseCode(ninjaResponse.HttpStatusCode)
                .DueTo(ninjaResponse.Data);
    }

    [Function("CreateNinja")]
    [OpenApiSecurity("x-functions-key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiOperation(operationId: "Create", tags: new[] {"Create Ninja"})]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Ninja))]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Ninja), Description = "Create new Ninja")]
    public async Task<HttpResponseData> CreateNinja([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
    {
        _logger.Information("Create Ninja request has been received");

        var response = req.CreateResponse();
        var readStream = new StreamReader(req.Body);
        var ninja = JsonSerializer.Deserialize<Ninja>(await readStream.ReadToEndAsync());
        
        await response.WriteAsJsonAsync(ninja, HttpStatusCode.Created);
        return response;
    }
    
    [Function("GetNinja")]
    [OpenApiOperation(operationId: "Get", tags: new[] { "Get Ninja" })]
    [OpenApiSecurity("x-functions-key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    [OpenApiParameter(name: "NinjaId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Id of Ninja to get")]
    public HttpResponseData Get([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        _logger.Information("Get Ninja request has been received");
            
        var ninjaId = req.Query["NinjaId"];

        if (int.TryParse(ninjaId, out var parsedNinjaId) is false)
            return new ShinobiApiResponse(req)
                .WithResponseCode(HttpStatusCode.BadRequest)
                .DueToMessage($"Problem processing {ninjaId}");
        
        var ninjaResponse = _shinobiService.Get(parsedNinjaId);

        if (ninjaResponse.IsSuccessful is false)
        {
            return new ShinobiApiResponse(req)
                .WithResponseCode(ninjaResponse.HttpStatusCode)
                .DueTo(ninjaResponse.Message);
        }
        
        return new ShinobiApiResponse(req)
            .WithResponseCode(HttpStatusCode.OK)
            .DueTo(ninjaResponse.Data);
    }
    
    [Function("SeedNinja")]
    [OpenApiOperation(operationId: "Get", tags: new[] { "Seed Ninjas" })]
    [OpenApiSecurity("x-functions-key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    [OpenApiParameter(name: "NumberToSeed", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Number of Random Ninjas to Create")]
    public HttpResponseData Seed([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        _logger.Information("Seed Ninja request has been received");
            
        var numberToSeed = req.Query["NumberToSeed"];

        if (int.TryParse(numberToSeed, out var parsedNumberToSeed) is false)
            return new ShinobiApiResponse(req)
                .WithResponseCode(HttpStatusCode.BadRequest)
                .DueToMessage($"Problem processing {numberToSeed}");
        
        var ninjaResponse = _shinobiService.Seed(parsedNumberToSeed);

        if (ninjaResponse.IsSuccessful is false)
        {
            return new ShinobiApiResponse(req)
                .WithResponseCode(ninjaResponse.HttpStatusCode)
                .DueTo(ninjaResponse.Message);
        }
        
        return new ShinobiApiResponse(req)
            .WithResponseCode(HttpStatusCode.Created)
            .DueTo(ninjaResponse.Data);
    }
}