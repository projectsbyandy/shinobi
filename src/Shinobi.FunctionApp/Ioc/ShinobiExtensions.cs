using Microsoft.Extensions.DependencyInjection;
using Shinobi.FunctionApp.Repository;
using Shinobi.FunctionApp.Repository.Internal;
using Shinobi.FunctionApp.Services;
using Shinobi.FunctionApp.Services.Internal;

namespace Shinobi.FunctionApp.Ioc;

public static class ShinobiExtensions
{
    public static IServiceCollection AddShinobiSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IShinobiRepository, FakeShinobiRepository>();
        serviceCollection.AddSingleton<IShinobiService, ShinobiService>();

        return serviceCollection;
    }
}