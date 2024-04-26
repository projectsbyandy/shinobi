using System.Net;
using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.Azure.Functions.Worker.Http;

namespace Shinobi.FunctionApp.Models;

public class ShinobiApiResponse(HttpRequestData httpRequestData)
{
    private HttpResponseData? _httpResponseData;

    public ShinobiApiResponse WithResponseCode(HttpStatusCode httpStatusCode)
    {
        _httpResponseData = httpRequestData.CreateResponse(httpStatusCode);

        return this;
    }

    public HttpResponseData DueTo<T>(T t)
    {
        Guard.Against.Null(_httpResponseData);
        Task.FromResult(_httpResponseData.WriteStringAsync(JsonSerializer.Serialize(t)));
        _httpResponseData.Headers.Add("Content-Type", "application/json");

        return _httpResponseData;
    }

    public HttpResponseData DueToMessage(string message)
    {
        Guard.Against.Null(_httpResponseData);
        Task.FromResult(_httpResponseData.WriteStringAsync(message));
        _httpResponseData.Headers.Add("Content-Type", "text/plain");

        return _httpResponseData;
    }
}