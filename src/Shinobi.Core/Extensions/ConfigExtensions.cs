using Ardalis.GuardClauses;
using Shinobi.Core.Models.Config;

namespace Shinobi.Core.Extensions;

public static class ConfigExtensions
{
    public static IServiceCollection AddConfigurationSupport(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var sqlConnectionDetails = configuration.GetSection("SqlConnectionDetails").Get<SqlConnectionDetails>();

        Guard.Against.Null(sqlConnectionDetails);
        
        serviceCollection.AddSingleton(sqlConnectionDetails);
        
        return serviceCollection;
    }
    
    public static IConfiguration GetConfiguration()
    {
        var env = Environment.GetEnvironmentVariable("ENVIRONMENTINTEST") ?? "development";

        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
    }
}