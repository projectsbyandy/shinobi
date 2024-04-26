using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Shinobi.FunctionApp.Ioc;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureLogging(configure =>
    {
        configure
            .ClearProviders()
            .Services
            .AddSerilog(
                new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger())
            .AddShinobiSupport();
    })
    .ConfigureOpenApi()
    .Build();

host.Run();