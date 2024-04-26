using System.Net;

namespace Shinobi.FunctionApp.Models;

public class ShinobiServiceResponse<T>(HttpStatusCode httpStatusCode)
{
    public bool IsSuccessful => HttpStatusCode.IsSuccessStatusCode();
    public string? Message { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; } = httpStatusCode;
    public T? Data { get; set; }
}